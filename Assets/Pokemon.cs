using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public abstract class Pokemon
{
    protected string pokemonName;
    protected int maxPokemonHP;
    protected int pokemonHP;
    protected List<Attack> pokemonAttacks;
    protected Player player;
    GameObject pokemonModel;
    public bool visible=true;

    public Pokemon(Player myPlayer, GameObject myModel)
    {
        player = myPlayer;
        pokemonModel = myModel;
        pokemonAttacks = new List<Attack>();

        if (pokemonModel == null)
        {
            Debug.LogError("Pokemon model is null!");
            return;
        }
    }

    public int getMaxHP()
    {
        return maxPokemonHP;
    }
    public int getHP()
    {
        return pokemonHP;
    }
    public string getName()
    {
        return pokemonName;
    }
    public GameObject getModel()
    {
        return pokemonModel;
    }
    public void damagePokemon(int damageAmount)
    {
        if (pokemonHP - damageAmount <= 0)
        {
            pokemonHP = 0;
            killPokemon();
        }
        else
        {
            pokemonHP = pokemonHP - damageAmount;
        }
        Debug.Log("Pokemon damaged");
        Debug.Log(pokemonName + " damaged, now has "+pokemonHP+" hp");
    }
    public void hidePokemon()
    {
        pokemonModel.SetActive(false);
        visible = false;
        Debug.Log(pokemonName + " is hidden");
    }
    public void unhidePokemon()
    {
        pokemonModel.SetActive(true);
        visible = true;
        Debug.Log(pokemonName + " is visible");

    }
    public void killPokemon()
    {
        Debug.Log(pokemonName + " died");
        player.removePokemon(this);
    }
    public void healPokemon(int healAmount)
    {
        if (pokemonHP + healAmount >= maxPokemonHP)
        {
            pokemonHP = maxPokemonHP;
        }
        else
        {
            pokemonHP = pokemonHP + healAmount;
        }
        Debug.Log("Pokemon healed "+pokemonHP+" hp");
    }
    public void attack(List<string> energies, Pokemon otherPokemon)
    {
        Debug.Log("inside attack function");
        Attack currentAttack=null;
        foreach(Attack attack in pokemonAttacks)
        {
            var firstNotSecond = energies.Except(attack.energiesNeeded).ToList();
            var secondNotFirst = attack.energiesNeeded.Except(energies).ToList();

            if (!firstNotSecond.Any()&& !secondNotFirst.Any())
            {
                currentAttack = attack;
            }
        }
        if (currentAttack != null)
        {
            int attackPower = currentAttack.attackPower;
            Debug.Log("Pokemon attacked");
            ParticleSystem[] particle = pokemonModel.GetComponentsInChildren<ParticleSystem>(true);
            if (particle.Length != 0)
            {
                particle[0].Play();
                if (particle.Length == 2)
                {
                    particle[1].Play();
                }
            }
            AudioSource[] source = pokemonModel.GetComponentsInChildren<AudioSource>(true);
            if (source.Length != 0)
            {
                source[0].Play();
            }
            otherPokemon.damagePokemon(attackPower);
        }

    }
}
