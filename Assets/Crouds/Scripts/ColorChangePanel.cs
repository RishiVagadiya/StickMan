using UnityEngine;

public class ColorChangePanel : MonoBehaviour
{
    public Material colorSourceMaterial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Followers"))
        {
            if (colorSourceMaterial == null)
            {
                Debug.LogWarning("❌ Material assign नहीं किया गया है!");
                return;
            }

            Color newColor = colorSourceMaterial.color;
            newColor.a = 1f;

            // ✅ सभी child renderers को पकड़ो (active/inactive दोनों को)
            MeshRenderer[] allRenderers = other.GetComponentsInChildren<MeshRenderer>(true);

            foreach (MeshRenderer rend in allRenderers)
            {
                if (rend != null)
                {
                    MaterialPropertyBlock mpb = new MaterialPropertyBlock();
                    rend.GetPropertyBlock(mpb);
                    mpb.SetColor("_Color", newColor);
                    rend.SetPropertyBlock(mpb);
                }
            }

            Debug.Log($"✅ {other.name} को नया रंग लगाया गया: {newColor}");
        }
    }
}
