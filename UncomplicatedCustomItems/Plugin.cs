using System;
using HarmonyLib;
using System.IO;
using UncomplicatedCustomItems.Events;
using UncomplicatedCustomItems.API.Features.Helper;
using UncomplicatedCustomItems.HarmonyElements.Patches;
using Handler = UncomplicatedCustomItems.Events.EventHandler;
using UnityEngine;
using System.Threading.Tasks;
using LabApi;
using LabApi.Events.Handlers;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Plugins;
using LabApi.Features;
using LabApi.Loader.Features.Plugins.Enums;

namespace UncomplicatedCustomItems
{
    public class Plugin : Plugin<Config>
    {
        public const bool IsPrerelease = true;
        public override string Name => "UncomplicatedCustomItems";

        public override string Description { get; } = "UCI simplifies the creation of customitems.";

        public override string Author => "SpGerg, FoxWorn & Mr. Baguetter";

        public override Version RequiredApiVersion { get; } = new Version(LabApiProperties.CompiledVersion);

        public override Version Version { get; } = new(3, 0, 0);

        internal Handler Handler;

        public override LoadPriority Priority => LoadPriority.High;

        public static Plugin Instance { get; private set; }

        public Harmony _harmony;

        internal static HttpManager HttpManager;

        internal FileConfig FileConfig;

        public override void Enable()
        {
            Instance = this;

            _harmony = new($"com.ucs.uci_exiled-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}");
            _harmony.PatchAll();

            FileConfig = new();
            HttpManager = new("uci");
            Handler = new();

            if (!File.Exists(Path.Combine(ConfigPath, "UncomplicatedCustomItems", ".nohttp")))

            if (IsPrerelease)
            {
                ServerEvents.WaitingForPlayers += Handler.OnWaitingForPlayers;
            }

            PlayerEvents.Hurt += Handler.OnHurt;
            //PlayerEvents.TriggeringTesla += Handler.OnTriggeringTesla;
            PlayerEvents.ShootingWeapon += Handler.OnShooting;
            PlayerEvents.UsedItem += Handler.OnItemUse;
            //PlayerEvents.ChangingAttachments += Handler.OnChangingAttachments;
            //ServerEvents.ActivatingWorkstation += Handler.OnWorkstationActivation;
            PlayerEvents.DroppedItem += Handler.OnDrop;
            //ServerEvents.PickupDestroyed += Handler.OnPickup;

            LogManager.History.Clear();

            LogManager.Info("===========================================");
            LogManager.Info(" Thanks for using UncomplicatedCustomItems");
            LogManager.Info("    by SpGerg, FoxWorn & Mr. Baguetter");
            LogManager.Info("===========================================");
            LogManager.Info(">> Join our discord: https://discord.gg/5StRGu8EJV <<");

            Events.Internal.Player.Register();
            Events.Internal.Server.Register();

            Task.Run(delegate
            {
                if (HttpManager.LatestVersion.CompareTo(Version) > 0)
                    LogManager.Warn($"You are NOT using the latest version of UncomplicatedCustomItems!\nCurrent: v{Version} | Latest available: v{HttpManager.LatestVersion}\nDownload it from GitHub: https://github.com/UncomplicatedCustomServer/UncomplicatedCustomItems/releases/latest");

                VersionManager.Init();
            });

            FileConfig.Welcome(loadExamples:true);
            FileConfig.Welcome(Server.Port.ToString());
            FileConfig.LoadAll();
            FileConfig.LoadAll(Server.Port.ToString());
        }

        public override void Disable()
        {
            Events.Internal.Player.Unregister();
            Events.Internal.Server.Unregister();

            HttpManager.UnregisterEvents();
            _harmony.UnpatchAll();
            _harmony = null;

            PlayerEvents.Hurt -= Handler.OnHurt;
            //PlayerEvents.TriggeringTesla -= Handler.OnTriggeringTesla;
            PlayerEvents.ShootingWeapon -= Handler.OnShooting;
            ServerEvents.WaitingForPlayers -= Handler.OnWaitingForPlayers;
            PlayerEvents.UsedItem -= Handler.OnItemUse;
            //PlayerEvents.ChangingAttachments -= Handler.OnChangingAttachments;
            //PlayerEvents.ActivatingWorkstation -= Handler.OnWorkstationActivation;
            PlayerEvents.DroppedItem -= Handler.OnDrop;
            //ServerEvents.PickupDestroyed -= Handler.OnPickup;

            Instance = null;
        }
    }
}