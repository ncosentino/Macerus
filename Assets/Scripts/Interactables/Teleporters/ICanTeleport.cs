namespace Assets.Scripts.Interactables.Teleporters
{
    public interface ICanTeleport
    {
        #region Methods
        void Teleport(ITeleportProperties teleportProperties);
        #endregion
    }
}
