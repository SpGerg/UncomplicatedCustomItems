﻿using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Loader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UncomplicatedCustomItems.API.Features.SpecificData;
using UnityEngine;

namespace UncomplicatedCustomItems.API.Features.Helper
{
    internal class FileConfig
    {
        private static readonly List<YAMLCustomItem> _examples =
        [
            new()
            {
                CustomData = YAMLCaster.Encode(new ItemData()
                {
                    Event = ItemEvents.Command,
                    Command = "/SERVER_EVENT DETONATION_INSTANT",
                    ConsoleMessage = "UHUHUHUH!"
                })
            },
            new()
            {
                Id = 2,
                Name = "MagicWeapon",
                Description = "A magic weapon with an incredible firerate",
                Item = ItemType.GunCOM18,
                CustomItemType = CustomItemType.Weapon,
                Scale = Vector3.one,
                CustomData = YAMLCaster.Encode(new WeaponData())
            },
            new()
            {
                Id = 3,
                Name = "Titanium Armor",
                Description = "A super heavy armor",
                Item = ItemType.ArmorHeavy,
                CustomItemType = CustomItemType.Armor,
                Scale = Vector3.one,
                CustomData = YAMLCaster.Encode(new ArmorData()
                {
                    HeadProtection = 150,
                    BodyProtection = 200,
                    RemoveExcessOnDrop = false,
                    StaminaUseMultiplier = 2
                })
            },
            new()
            {
                Id = 4,
                Name = "Incredible beautiful keycard",
                Description = "UWU owo keycard",
                Item = ItemType.KeycardJanitor,
                CustomItemType = CustomItemType.Keycard,
                Scale = Vector3.one,
                CustomData = YAMLCaster.Encode(new KeycardData()
                {
                    Permissions = KeycardPermissions.AlphaWarhead | KeycardPermissions.Checkpoints
                })
            },
            new()
            {
                Id = 5,
                Name = "My favourite grenade",
                Description = "Throw it my friend :)",
                Item = ItemType.GrenadeHE,
                CustomItemType = CustomItemType.ExplosiveGrenade,
                Scale = Vector3.one,
                CustomData = YAMLCaster.Encode(new ExplosiveGrenadeData()
                {
                    MaxRadius = 250f
                })
            },
            new()
            {
                Id = 6,
                Name = "Blinder",
                Description = "Make every people in the facility blind",
                Item = ItemType.GrenadeFlash,
                CustomItemType = CustomItemType.FlashGrenade,
                Scale = Vector3.one,
                CustomData = YAMLCaster.Encode(new FlashGrenadeData()
                {
                    AdditionalBlindedEffect = 250f
                })
            },
            new()
            {
                Id = 7,
                Name = "Overpowered medikit",
                Description = "This medikit will heal you 100%",
                Item = ItemType.Medkit,
                CustomItemType = CustomItemType.Medikit,
                Scale = new(2, 2, 2),
                CustomData = YAMLCaster.Encode(new MedikitData()
                {
                    Health = 250f
                })
            },
            new()
            {
                Id = 8,
                Name = "Really fast painkillers",
                Description = "This painkillers regenerate lots of health within seconds but you'll have to wait...",
                Item = ItemType.Painkillers,
                CustomItemType = CustomItemType.Painkillers,
                Scale = new(5, 5, 5),
                CustomData = YAMLCaster.Encode(new PainkillersData()
                {
                    TickHeal = 1f,
                    TickTime = 0.1f,
                    TimeBeforeStartHealing = 10f,
                    TotalHealing = 50f
                })
            },
            new()
            {
                Id = 9,
                Name = "Shitty adrenaline",
                Description = "This adrenaline just give you 10AHP",
                Item = ItemType.Adrenaline,
                CustomItemType = CustomItemType.Adrenaline,
                Scale = Vector3.one,
                CustomData = YAMLCaster.Encode(new AdrenalineData()
                {
                    Amount = 10,
                    Decay = 0.01f,
                    Persistant = true,
                    Sustain = 1000
                })
            }
        ];

        internal string Dir = Path.Combine(Paths.Configs, "UncomplicatedCustomItems");

        public bool Is(string localDir = "")
        {
            return Directory.Exists(Path.Combine(Dir, localDir));
        }

        public string[] List(string localDir = "")
        {
            return Directory.GetFiles(Path.Combine(Dir, localDir));
        }

        public void LoadAll(string localDir = "")
        {
            LoadAction((YAMLCustomItem Item) =>
            {
                CustomItem.Register(YAMLCaster.Converter(Item));
            }, localDir);
        }

        public void LoadAction(Action<YAMLCustomItem> action, string localDir = "")
        {
            foreach (string FileName in List(localDir))
            {
                try
                {
                    if (Directory.Exists(FileName))
                        continue;

                    if (FileName.Split().First() == ".")
                        return;

                    YAMLCustomItem Role = Loader.Deserializer.Deserialize<YAMLCustomItem>(File.ReadAllText(FileName));
                    LogManager.Debug($"Proposed to the registerer the external item {Role.Id} [{Role.Name}] from file:\n{FileName}");
                    action(Role);
                }
                catch (Exception ex)
                {
                    if (!Plugin.Instance.Config.Debug)
                    {
                        LogManager.Error($"Failed to parse {FileName}. YAML Exception: {ex.Message}.");
                    }
                    else
                    {
                        LogManager.Error($"Failed to parse {FileName}. YAML Exception: {ex.Message}.\nStack trace: {ex.StackTrace}");
                    }
                }
            }
        }

        public void Welcome(string localDir = "", bool loadExamples = false)
        {
            if (!Is(localDir))
            {
                Directory.CreateDirectory(Path.Combine(Dir, localDir));
                if (!loadExamples)
                    File.WriteAllText(Path.Combine(Dir, localDir, "example-item.yml"), Loader.Serializer.Serialize(new YAMLCustomItem()
                    {
                        Id = CustomItem.GetFirstFreeId(1)
                    }));
                else
                    foreach (YAMLCustomItem customItem in _examples)
                        File.WriteAllText(Path.Combine(Dir, localDir, $"{customItem.Name.ToLower().Replace(" ", "-")}.yml"), Loader.Serializer.Serialize(customItem));

                LogManager.Info($"Plugin does not have a role folder, generated one in {Path.Combine(Dir, localDir)}");
            }
        }
    }
}