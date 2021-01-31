namespace UnityEngine.XR.Interaction.Toolkit
{
    public class TeleportParentAnchor : BaseTeleportationInteractable
    {
        [SerializeField]
        Transform m_TeleportAnchorTransform;

        [SerializeField]
        protected bool m_ShouldParent = true;

        /// <summary>
        /// The <see cref="Transform"/> that represents the teleportation destination.
        /// </summary>
        public Transform teleportAnchorTransform
        {
            get => transform;
            set => m_TeleportAnchorTransform = value;
        }

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        protected void OnValidate()
        {
            if (m_TeleportAnchorTransform == null)
                m_TeleportAnchorTransform = transform;
        }

        /// <summary>
        /// Called when gizmos are drawn.
        /// </summary>
        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            GizmoHelpers.DrawWireCubeOriented(m_TeleportAnchorTransform.position, m_TeleportAnchorTransform.rotation, 1f);

            GizmoHelpers.DrawAxisArrows(m_TeleportAnchorTransform, 1f);
        }

        /// <inheritdoc />
        protected override bool GenerateTeleportRequest(XRBaseInteractor interactor, RaycastHit raycastHit, ref TeleportRequest teleportRequest)
        {
            if (m_TeleportAnchorTransform == null)
                return false;

            teleportRequest.destinationPosition = m_TeleportAnchorTransform.position;
            teleportRequest.destinationRotation = m_TeleportAnchorTransform.rotation;

            XRRigSingleton.Instance.gameObject.transform.parent = transform;

            return true;
        }
    }
}