using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Name_Generator : MonoBehaviour
{
    public static string[] adjectives = new string[] {
        "Dark","Moist","Sporadic","Surreptitious","Succulent","Wet","Spikey","Hungry","Angry","Slimy","Chonky","Precious",
        "Pretentious","Sloshy","Picquant","Clement","Funny","Norwegian","Kindred","Juberous","Newfangled","Bouncy","Ineffable",
        "Unfunny","Mad","Soft","Belligerent","Terrific","Brainy","Shallow","Skinny","Painful","Crabby","Sweet","Exotic","Dramatic",
        "Cute","Fluffy","Quaint","Curly","Dizzy","Mighty","Misty","Tiny","Acidic","Violet","Scrawny","Animated","Colorful",
        "Flowery","Tart","Shiny","Forgetful","Woozy","Dangerous","Sharp", "Sour", "Absurd", "Naive", "Overloaded", "Edgy", "Explosive",
        "Epic", "Desolate", "Hyper", "Frozen", "Caustic", "Burning", "Telepathic", "Wicked", "Whimsical", "Perplexing", "Smooth", "Determined",
        "Deadly", "Ludicrous", "Objective", "Subjective", "Red", "Blue", "Pink", "Powerful", "Awful", "Silent", "Friendly", "Solid", "Liquid",
        "Gaseous", "Electrical", "Chaotic", "Lemony", "Volatile", "Flying", "Aerodynamic", "Annoying", "Skittish", "Conductive", "Salty",
        "Warm", "Scorching", "Brittle", "Zealous", "Crazed", "Insane", "Comatosed", "Sleepy", "Stringy", "Luminous", "Extraordinary", "Tormented",
        "Gnarly", "Ingeneous", "Painterly", "Broken", "Gorgeous", "Despicable", "Steamy", "Messy", "Jovial", "Bumpy", "Rounded", "Sugary", "Malicious",
        "Musical", "Hostile", "Crunchy", "Preposturous", "Melancholic", "Savage", "Clumsy", "Fragrant", "Smelly", "Stinky", "Funky", "Expansive"
    };

    public static string[] nouns = new string[] {
        "Light","Engine","Soup","Judge","Tongue","Woofer","Quack","Jugular","Dog","Cat","Spider","Hatchet","Adventurer","Guitar",
        "Slime","Archer","Relish","Tank","Author","Steak","Otter","Shadow","Watermelon","Macaroon","Jackal","Existence","Cow",
        "Skeleton","Pizza","Calzone","Rat","Sheep","Pickle","Juice","Snail","Oatmeal","Brick","Train","Twig","Cabbage","Marble",
        "Volleyball","Airplane","Queen","Hammer","Calculator","Eggnog","Sheeb","Sheep","Blatherskite","Zinger","Squib","Squid",
        "Gorp","Ambivert","Norwegian", "Paint", "Bees", "Lemon", "Comquat", "Goose", "Toast", "Waffle", "Water", "Speed", "Book",
        "Lamp", "Pie", "Hornet", "Snake", "Platypus", "Tree", "Gamer", "Imposter", "Beans", "Pencil", "Wizard", "Wolf", "Mold", "Grass",
        "Ghost", "Explorer", "Milk", "Sauce", "Cake", "Bird", "Chicken", "Knight", "Mouse", "Cup", "Soda", "Cereal", "Mug", "Fridge", "Watch",
        "Sweater", "Fighter", "Vampire", "Device", "Bottle", "Microwave", "Ninja", "Beast", "Warlock", "Cutter", "Wire", "Blender", "Wallet",
        "Monster", "Boxer", "Airplane", "Tsunami", "Hurricane", "Storm", "Mastermind", "IceCube", "Cretin", "Vegetable", "Crossiant", "Raccoon",
        "Melon", "Coconut", "Charlatan", "Faker", "Hedgehog", "Peanut", "Ranger", "Druid", "Gnome", "Lion", "Avocado", "Citizen", "Hero", "Shampoo",
        "Dolphin", "Shark", "Needle", "Coffin", "Tomb", "Emperor", "Joker", "Bug", "Beetle", "Butterfly", "Moth"
    };

    public static string Generate_Name()
    {
        return adjectives[Random.Range(0, adjectives.Length)] + nouns[Random.Range(0, nouns.Length)] + Random.Range(0, 1000);
    }
}
