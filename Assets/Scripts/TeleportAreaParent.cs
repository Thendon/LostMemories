namespace UnityEngine.XR.Interaction.Toolkit
{

    /// <summary>
    /// An area is a teleportation destination which teleports the user to their pointed
    /// location on a surface.
    /// </summary>
    /// <seealso cref="TeleportationAnchor"/>
    [HelpURL(XRHelpURLConstants.k_TeleportationArea)]
    public class TeleportAreaParent : BaseTeleportationInteractable
    {
        PlayerVR player = null;

        protected override void Awake()
        {
            base.Awake();
            player = FindObjectOfType<PlayerVR>();
        }

        /// <inheritdoc />
        protected override bool GenerateTeleportRequest(XRBaseInteractor interactor, RaycastHit raycastHit, ref TeleportRequest teleportRequest)
        {
            teleportRequest.destinationPosition = raycastHit.point;
            teleportRequest.destinationRotation = transform.rotation;

            XRRigSingleton.Instance.gameObject.transform.parent = null;

            player.ReleasePlayer();

            return true;
        }
    }
}