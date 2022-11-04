using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Misc
{

    //0 is generic
    //1 is funny
    //2 is Aggresive
    //5 is male
    //6 is female
    //10 is generic vendor
    //20 is Miner
    //30 is drunk
    //40 is magic
    //50 is Inn and barkeep
    //60 is Smith
    //70 is Healer
    public class Speech
    {
        // 0
        private static Dictionary<int, string>  genericStatments = new Dictionary<int, string>()
            {
                {1, "The weather is lovely today"},
                {2, "The weather is hot today"},
                {3, "Need Something?"},
                {4, "Yes?"},
                {5, "Huh"},
                {6, "Your the best!"},
                {7, "Hi there"},
                {8, "I bet you could slay one of those mean old dragons."},
                {9, "Good to see you again, friend."},
                {10, "Now here's a person I'm glad to see"},
                {11, "Something you need, you miserable wretch"},
                {12, "Peace for you." },
                {13, "Peace to you." },
                {14, "Long life my friend" },
                {15, "Safe travels" },
                {16, "Keep your feet on the ground." },
                {17, "Trust no one." },
                {18, "Have you ever seen Lord British?" },
                {19, "I can't remember the last time I had, well, anything." },
                {20, "Can't you see I'm busy?" },
                {21, "Excuse me." },
                {22, "What's the point?" },
                {23, "Why go on?" },
                {24, "I'm gonna be sick." },
                {25, "This is bad" },
                {26, "You're not the person I thought you were." },
                {27, "I don' think this war's ever gonna end." },
                {28, "I don't dream anymore" },
                {29, "I could eat a horse, hooves and all." },
                {30, "Looks like things are getting worse, not better." },
                {31, "If I could live my life over again..." },
                {32, "I can't remember the last time I cleaned myself" },
                {33, "Doesn't anyone care what I think?" },
                {34, "I can't get this tune out of my head." },
                {35, "I just knew it was gonna be on of those days" },
                {36, "I ate something bad..." },
                {37, "God I'm hungry" },
                {38, "I'm glad there's no kids around to see this." },
                {39, "That almost made sense" },
                {40, "Couldn't have put it better myself" },
                {41, "Stop looking at me like that." },
                {42, "Watch what you're doing." },
                {43, "Know what you mean." },
                {44, "You're talking to yourself again" },
                {45, "I wouldn't say that too loud" },
                {46, "I'm not even gonna tell you to shut up." },
                {47, "Whoa...deja vu!" },
                {48, "You smell that?" },
                {49, "I heard wisps fight the undead" },
                {50, "I heard somewhere giant snakes and spiders fight" },
                {51, "I heard the ratmen and orcs are at war" },
                {52, "I never knew Lord British's first name is Cantabrigian." },
                {53, "Rare Unqiue items are said to appear in dugneons around the land" },
                {54, "Legend states that a rare beast appears in the depths of Wrong" },
                {55, "Is Lord Black Thron a bad guy?" },
                {56, "Do you live in a castle?"},

            };
        //1
        private static Dictionary<int, string> humorousStatments = new Dictionary<int, string>()
            {
                {1, "Do harpies have babies or eggs?"},
                {2, "When centaurs mate, is there a chance that a horse is born?"},
                {3, "When centaurs mate, is there a chance that a person is born?"},
                {4, "Have you noticed that all trolls are male?"},
                {5, "Have you noticed that all ettins are male?"},
                {6, "If a centaur and a mermaid mate, would they create a seahorse?"},
                {7, "Did you pick out your clothes in the dark? Or perhaps from a trash can?"},
                {8, "Lalalala… Lalalala… I am a loner…"},
                {9, "Fooooo!"},
                {10, "EEK! Did you touch me?" },
                {11, "I AM GOING TO ENJOY AN EGG" },
                {12, "Here come the hacks!" },
                {13, "Sometiems I dream about cheese" },
                {14, "To think, all I used to want to do was sell insurance." },

            };
        //2
        private static Dictionary<int, string> aggresiveStatments = new Dictionary<int, string>()
            {
                {1, "I should bash your face in!"},
                {2, "I hope monsters come and eat you!"},
                {3, "Something you need, you miserable wretch"},
                {4, "What are you starin' at?"},
                {5, "Gods strike you down!"},
                {6, "What are you babbling about?"},
                {7, "Run away pest!"},
                {8, "Not very intelligent, are you?"},
                {9, "Ah, you have a death wish."},
                {10, "You behave like a child!" },
                {11, "Your mother was a troll!" },
                {12, "Watch yer back!" },
                {13, "Y'all are stupid!" },
                {14, "How's that? Owned, suckah!" },
                {15, "Get the hell out of here!" },
                {16, "Something must be wrong with me, I almost understood that." },
                {17, "Have you ever had an original thought?" },
                {18, "I'll put it on your tombstone." },

            };
        //5
        private static Dictionary<int, string> maleStatments = new Dictionary<int, string>()
            {
                {1, "I'm a cool guy, I have a girlfriend!"},
                {2, "I'm a Man"}
            };
        //6
        private static Dictionary<int, string> femaleStatments = new Dictionary<int, string>()
            {
                {1, "Why do men like bathing suits so much?"},
                {2, "It's a woman thing, you wouldn't understand"},
                {3, "Keep your mind on your work." },
                {4, "Your mind is in the gutter" },
                {5, "When a man is wrong and won't admit is, he always gets angry." }
            };

        // 10
        private static Dictionary<int, string> vendorStatments = new Dictionary<int, string>()
            {
                {1, "Take a look."},
                {2, "Diggin' in des mines"},
                {3, "Oh, a bit of this and a bit of that." },
                {4, "Trinkets, odds and ends, that sort of thing." },
                {5, "Some may call this junk. Me, I call them treasures."},
                {6, "Choose wisely."},
                {7, "Do not loiter."},
                {8, "Everything has a price."},
                {9, "I have one of a kind items"},
                {10, "What do you seek?"},
                {11, "Your gold is welcome here."},
                {12, "Buy? Trade?" },
                {13, "You find many things here!" },
                {14, "Take time if you need." },
                {15, "Good prices here!" },
                {16, "All prices here are reasonable" },
                {17, "Please browse my wares." },
                {18, "Sale here." }
            };

        // 20
        private static Dictionary<int, string> minerStatments = new Dictionary<int, string>()
            {
                {1, "I'm a miner"},
                {2, "Diggin' in des mines"},
                {3, "I'm here to mine. So unless you want to dig, get out."},
                {4, "Good ore brings strength to our people."},
                {5, "The mines are hard, but rewards those who rise to the challenge."},
            };

        // 30
        private static Dictionary<int, string> drunkStatments = new Dictionary<int, string>()
            {
                {1, "My favorite drinking buddy. Let's get some mead."},
                {2, "Interest ya'n a pint?"},
                {3, "I don't feel anything anymore" }
            };

        // 40
        private static Dictionary<int, string> magicVendorStatments = new Dictionary<int, string>()
            {
                {1, "Got a pretty full stock of potions and alchemy reagents."},
                {2, "Ah, so you're an alchemist, then?"},
                {3, "Spells and incantations for those with the talent to cast them."},
                {4, "So, you wish to master the arcane arts..."},
                {5, "Hmph. I had you figured for a mage. I think you'll appreciate this..."},
            };

        //50
        private static Dictionary<int, string> innAndBarkeepStatments = new Dictionary<int, string>()
            {
                {1, "Drink for the thirsty, food for the hungry."},
                {2, "Let's sate that appetite, hmm?"},
                {3, "Depends. Are you thirsty, hungry, both?"},
                {4, "Let's sate that appetite, hmm?"},
            };

        //60
        private static Dictionary<int, string> smithStatments = new Dictionary<int, string>()
            {
                {1, "The finest weapons and armor."},
                {2, "Hmm... Blades, helmets. Pretty much anything to suit your needs."},
                {3, "Looking to protect yourself, or deal some damage?"},
            };

        //70
        private static Dictionary<int, string> healerStatments = new Dictionary<int, string>()
            {
                {1, "Blessings upon your family"},
                {2, "Good fortune"},
                {3, "Do you require medication?"},
                {4, "Each day is a blessing"},
                {5, "May the light embrace you"},
                {6, "May your days be long, and your hardships few."},
                {7, "Blessing upon your family." },
                {8, "How can I help?" },
                {9, "Light's blessing to you" }
            };

        public static string getSpeech(List<int> Statments )
        {
            var rand = new Random();

            Statments.Add(0);

            //int index = rand.Next(Statments.Count);
            int index = Utility.Random(Statments.Count);

            switch (index)
            {
                default:
                    return genericStatments.ElementAt(Utility.Random(0, genericStatments.Count)).Value;
                    break;
                case 0:
                    return genericStatments.ElementAt(Utility.Random(0, genericStatments.Count)).Value;
                    break;
                case 1:
                    return humorousStatments.ElementAt(Utility.Random(0, humorousStatments.Count)).Value;
                    break;
                case 2:
                    return aggresiveStatments.ElementAt(Utility.Random(0, aggresiveStatments.Count)).Value;
                    break;
                case 5:
                    return maleStatments.ElementAt(Utility.Random(0, maleStatments.Count)).Value;
                    break;
                case 6:
                    return femaleStatments.ElementAt(Utility.Random(0, femaleStatments.Count)).Value;
                    break;
                case 10:
                    return vendorStatments.ElementAt(Utility.Random(0, vendorStatments.Count)).Value;
                    break;
                case 20:
                    return minerStatments.ElementAt(Utility.Random(0, minerStatments.Count)).Value;
                    break;
                case 30:
                    return drunkStatments.ElementAt(Utility.Random(0, drunkStatments.Count)).Value;
                    break;
                case 40:
                    return magicVendorStatments.ElementAt(Utility.Random(0, magicVendorStatments.Count)).Value;
                    break;
                case 50:
                    return innAndBarkeepStatments.ElementAt(Utility.Random(0, innAndBarkeepStatments.Count)).Value;
                    break;
                case 60:
                    return smithStatments.ElementAt(Utility.Random(0, smithStatments.Count)).Value;
                    break;
                case 70:
                    return healerStatments.ElementAt(Utility.Random(0, healerStatments.Count)).Value;
                    break;
            }

        }
    }

}