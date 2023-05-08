package main

import (
	"bufio"
	"encoding/csv"
	"flag"
	"fmt"
	"io/ioutil"
	"log"
	"os"
	"regexp"
	"strconv"
	"strings"
)

func main() {

	// Provide Flags and info
	generateMonsterExcelFile := flag.Bool("MonsterExcelFile", false,
		"The MonsterExcelFile flag takes a true flag to run and output an excel file")

	flag.Parse()

	fmt.Println("MonsterExcelFile:", *generateMonsterExcelFile)

	if *generateMonsterExcelFile {
		GenerateMonsterExcelFile()
	}

}

type monsterParamters struct {
	name          string
	str           string
	damage        string
	hits          string
	dex           string
	intelligence  string
	magery        string
	resistance    string
	tactics       string
	poisonImmune  string
	hasFireBreath bool
	hitPoison     string
}

type monsterCleanedData struct {
	name          string
	strMin        int
	strMax        int
	damageMin     int
	damageMax     int
	hitsMin       int
	hitsMax       int
	dexMin        int
	dexMax        int
	intMin        int
	intMax        int
	magery        float64
	resistance    float64
	tactics       float64
	poisonImmune  int
	hitPoison     int
	hasFireBreath bool
}

func GenerateMonsterExcelFile() error {
	path := "./Scripts/Mobiles/Monsters"
	monDataList, err := cycleThroughFiles(path)
	if err != nil {
		log.Default().Println(err)
	}

	monsterCleanedData := cleanData(monDataList)

	err = writeCSV(monsterCleanedData)

	fmt.Println(monsterCleanedData)
	return nil
}

func cycleThroughFiles(path string) ([]monsterParamters, error) {
	monData := []monsterParamters{}
	files, err := ioutil.ReadDir(path)
	if err != nil {
		return []monsterParamters{}, err
	}
	for _, f := range files {
		if f.IsDir() {
			tempData1, err := cycleThroughFiles(path + "/" + f.Name())
			if err != nil {
				log.Default().Println(err)
			}
			monData = append(monData, tempData1...)
		} else {
			tempData, err := readFileData(path + "/" + f.Name())
			if err != nil {
				log.Default().Println(err)
			}
			monData = append(monData, tempData)
		}
	}
	return monData, nil
}

func readFileData(path string) (monsterParamters, error) {
	data := monsterParamters{}
	f, err := os.Open(path)
	if err != nil {
		return monsterParamters{}, err
	}
	defer f.Close()

	// Splits on newlines by default.
	scanner := bufio.NewScanner(f)

	line := 1
	// https://golang.org/pkg/bufio/#Scanner.Scan
	for scanner.Scan() {
		if strings.Contains(scanner.Text(), "public class") {
			data.name = strings.TrimSpace(scanner.Text())
		} else if strings.Contains(scanner.Text(), "SetStr") {
			data.str = strings.TrimSpace(scanner.Text())
		} else if strings.Contains(scanner.Text(), "SetDamage") {
			data.damage = strings.TrimSpace(scanner.Text())
		} else if strings.Contains(scanner.Text(), "SetHits") {
			data.hits = strings.TrimSpace(scanner.Text())
		} else if strings.Contains(scanner.Text(), "SetDex") {
			data.dex = strings.TrimSpace(scanner.Text())
		} else if strings.Contains(scanner.Text(), "SetInt") {
			data.intelligence = strings.TrimSpace(scanner.Text())
		} else if strings.Contains(scanner.Text(), "SetSkill( SkillName.Magery") {
			data.magery = strings.TrimSpace(scanner.Text())
		} else if strings.Contains(scanner.Text(), "SetSkill( SkillName.MagicResist") {
			data.resistance = strings.TrimSpace(scanner.Text())
		} else if strings.Contains(scanner.Text(), "SetSkill( SkillName.Tactics") {
			data.tactics = strings.TrimSpace(scanner.Text())
		} else if strings.Contains(scanner.Text(), "Poison PoisonImmune") {
			data.poisonImmune = strings.TrimSpace(scanner.Text())
		} else if strings.Contains(scanner.Text(), "Poison HitPoison") {
			data.hitPoison = strings.TrimSpace(scanner.Text())
		} else if strings.Contains(scanner.Text(), "HasBreath") {
			data.hasFireBreath = true
		}

		line++
	}

	if err = scanner.Err(); err != nil {
		// Handle the error
		return monsterParamters{}, err
	}
	return data, nil
}

func cleanData(monDataList []monsterParamters) []monsterCleanedData {
	cD := []monsterCleanedData{}

	for _, data := range monDataList {
		tempCD := monsterCleanedData{}

		// Parse Name
		tempCutPrix, _ := strings.CutPrefix(data.name, "public class ")
		tempCutSufix, _ := strings.CutSuffix(tempCutPrix, " : BaseCreature")
		tempCD.name = tempCutSufix

		// Parse strMin and Max
		tempMin, tempMax := getMinMax(data.str)
		tempCD.strMin = tempMin
		tempCD.strMax = tempMax

		// Parse damageMin and Max
		tempMin, tempMax = getMinMax(data.damage)
		tempCD.damageMin = tempMin
		tempCD.damageMax = tempMax

		// Parse hitsMin and Max
		tempMin, tempMax = getMinMax(data.hits)
		tempCD.hitsMin = tempMin
		tempCD.hitsMax = tempMax

		// Parse dexMin and Max
		tempMin, tempMax = getMinMax(data.dex)
		tempCD.dexMin = tempMin
		tempCD.dexMax = tempMax

		// Parse intMin and Max
		tempMin, tempMax = getMinMax(data.intelligence)
		tempCD.intMin = tempMin
		tempCD.intMax = tempMax

		// Parse mageryMax
		tempMaxSkill := getSkillMax(data.magery)
		tempCD.magery = tempMaxSkill

		// Parse resistMax
		tempMaxSkill = getSkillMax(data.resistance)
		tempCD.resistance = tempMaxSkill

		// Parse tacticsMax
		tempMaxSkill = getSkillMax(data.tactics)
		tempCD.tactics = tempMaxSkill

		// Parse Poison Imunity
		tempCD.poisonImmune = getPoisonLevel(data.poisonImmune)

		// Parse Poison hit
		tempCD.hitPoison = getPoisonLevel(data.hitPoison)

		// finally firebreath
		tempCD.hasFireBreath = data.hasFireBreath

		// Add Cleaned Monster Data to list
		cD = append(cD, tempCD)
	}

	return cD
}

func getMinMax(data string) (int, int) {
	var min, max int
	re := regexp.MustCompile(`[-]?\d[\d]*[\.]?[\d{2}]*`)
	subMatchAll := re.FindAllString(data, -1)
	if len(subMatchAll) == 2 {
		min, _ = strconv.Atoi(subMatchAll[0])
		max, _ = strconv.Atoi(subMatchAll[1])
	}
	if len(subMatchAll) == 1 {
		min, _ = strconv.Atoi(subMatchAll[0])
		max, _ = strconv.Atoi(subMatchAll[0])
	}
	return min, max
}

func getSkillMax(data string) float64 {
	var max float64
	re := regexp.MustCompile(`[-]?\d[\d]*[\.]?[\d{2}]*`)
	subMatchAll := re.FindAllString(data, -1)
	if len(subMatchAll) == 2 {
		max, _ = strconv.ParseFloat(subMatchAll[1], 64)
	} else if len(subMatchAll) == 1 {
		max, _ = strconv.ParseFloat(subMatchAll[0], 64)
	}
	return max
}

// zero meaning none, six meaning lethal
func getPoisonLevel(data string) int {
	tempData := strings.ToLower(data)
	if strings.Contains(tempData, "lesser") {
		return 1
	}
	if strings.Contains(tempData, "regular") {
		return 2
	}
	if strings.Contains(tempData, "greater") {
		return 3
	}
	if strings.Contains(tempData, "deadly") {
		return 4
	}
	if strings.Contains(tempData, "lethal") {
		return 5
	}
	return 0
}

func writeCSV(dataItems []monsterCleanedData) error {

	// Create a new file to store CSV data
	outputFile, err := os.Create("./test.csv")
	if err != nil {
		return err
	}
	defer outputFile.Close()

	//  Write the header of the CSV file and the successive rows by iterating through the JSON struct array
	writer := csv.NewWriter(outputFile)
	defer writer.Flush()

	header := []string{"name", "strMin", "strMax", "damageMin", "damageMax", "hitsMin", "hitsMax", "dexMin",
		"dexMax", "intMin", "intMax", "magery", "resistance", "tactics", "poisonImmune", "hitPoison", "hasFireBreath"}
	if err := writer.Write(header); err != nil {
		return err
	}

	for _, r := range dataItems {
		var csvRow []string
		csvRow = append(csvRow, r.name, fmt.Sprint(r.strMin), fmt.Sprint(r.strMax), fmt.Sprint(r.damageMin), fmt.Sprint(r.damageMax),
			fmt.Sprint(r.hitsMin), fmt.Sprint(r.hitsMax), fmt.Sprint(r.dexMin), fmt.Sprint(r.dexMax), fmt.Sprint(r.intMin),
			fmt.Sprint(r.intMax), fmt.Sprint(r.magery), fmt.Sprint(r.resistance), fmt.Sprint(r.tactics), fmt.Sprint(r.poisonImmune),
			fmt.Sprint(r.hitPoison), fmt.Sprint(r.hasFireBreath))
		if err := writer.Write(csvRow); err != nil {
			return err
		}
	}
	return nil

}
