using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leafeon : Pokemon
{
    public Leafeon(Player myPlayer, GameObject myModel) : base(myPlayer, myModel)
    {
        pokemonName = "Leafeon";
        pokemonHP = 120;
        maxPokemonHP = 120;
        List<string> leafletBlessingsEnergies = new List<string> { "colorless" };
        Attack leafletBlessings = new Attack("LeafletBlessings", leafletBlessingsEnergies, 30, "colorless");
        pokemonAttacks.Add(leafletBlessings);
        List<string> solarBeamEnergies = new List<string> { "grass", "colorless" };
        Attack solarBeam = new Attack("SolarBeam", solarBeamEnergies, 70, "grass");
        pokemonAttacks.Add(solarBeam);
    }
}
