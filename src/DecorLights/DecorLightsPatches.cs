using HarmonyLib;
using STRINGS;

namespace DecorLights
{
    public static class DecorLightsPatches
    {
        private const string FurnitureCategory = "Furniture";
        private const string GlassFurnishingsTech = "GlassFurnishings";

        [HarmonyPatch(typeof(GeneratedBuildings), nameof(GeneratedBuildings.LoadGeneratedBuildings))]
        public static class GeneratedBuildings_LoadGeneratedBuildings_Patch
        {
            public static void Prefix()
            {
                RegisterBuilding(LavaLampConfig.Id, LavaLampConfig.DisplayName, LavaLampConfig.Description, LavaLampConfig.Effect);
                RegisterBuilding(SaltLampConfig.Id, SaltLampConfig.DisplayName, SaltLampConfig.Description, SaltLampConfig.Effect);
                RegisterBuilding(CeilingLampConfig.Id, CeilingLampConfig.DisplayName, CeilingLampConfig.Description, CeilingLampConfig.Effect);
                RegisterBuilding(LuminiferousSphereConfig.Id, LuminiferousSphereConfig.DisplayName, LuminiferousSphereConfig.Description, LuminiferousSphereConfig.Effect);
            }
        }

        [HarmonyPatch(typeof(Db), nameof(Db.Initialize))]
        public static class Db_Initialize_Patch
        {
            public static void Postfix()
            {
                var tech = Db.Get().Techs.Get(GlassFurnishingsTech);
                AddUnlockIfMissing(tech, LavaLampConfig.Id);
                AddUnlockIfMissing(tech, SaltLampConfig.Id);
                AddUnlockIfMissing(tech, CeilingLampConfig.Id);
                AddUnlockIfMissing(tech, LuminiferousSphereConfig.Id);
            }
        }

        private static void RegisterBuilding(string id, string name, string description, string effect)
        {
            var stringPrefix = $"STRINGS.BUILDINGS.PREFABS.{id.ToUpperInvariant()}";
            Strings.Add($"{stringPrefix}.NAME", UI.FormatAsLink(name, id));
            Strings.Add($"{stringPrefix}.DESC", description);
            Strings.Add($"{stringPrefix}.EFFECT", effect);
            ModUtil.AddBuildingToPlanScreen(FurnitureCategory, id);
        }

        private static void AddUnlockIfMissing(Tech tech, string buildingId)
        {
            if (!tech.unlockedItemIDs.Contains(buildingId))
                tech.unlockedItemIDs.Add(buildingId);
        }
    }
}
