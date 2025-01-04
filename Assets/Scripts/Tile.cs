using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int gridPosition;
    private SpriteRenderer spriteRenderer;
    private static Tile selectedTile;
    public Color tileColor { get; private set; }
    private AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip removeSound;
    public ParticleSystem clickEffect;
    public ParticleSystem removeEffect;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void SetColor(Color color)
    {
        tileColor = color;
        spriteRenderer.color = color;
    }

    void OnMouseDown()
    {
        PlayClickEffect();
        PlayClickSound(); 

        if (selectedTile == null)
        {
            SelectTile();
        }
        else
        {
            if (selectedTile != this)
            {
                GridManager gridManager = FindObjectOfType<GridManager>();

                if (!gridManager.CheckMatch(selectedTile, this))
                {
                    SwapTiles(selectedTile, this);
                }
            }
            selectedTile.DeselectTile();
            selectedTile = null;
        }
    }

    void SelectTile()
    {
        selectedTile = this;
        transform.localScale = Vector3.one * 0.85f;
    }

    void DeselectTile()
    {
        transform.localScale = Vector3.one * 0.98f;
    }

    void SwapTiles(Tile tile1, Tile tile2)
    {
        Vector2Int tempGridPosition = tile1.gridPosition;
        tile1.gridPosition = tile2.gridPosition;
        tile2.gridPosition = tempGridPosition;

        // Swap world positions
        Vector3 tempWorldPosition = tile1.transform.position;
        tile1.transform.position = tile2.transform.position;
        tile2.transform.position = tempWorldPosition;

        // Optionally update the GridManager to reflect the swap in the grid data
        GridManager gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            gridManager.UpdateGrid(tile1, tile2);
        }
    }
    public void RemoveTile()
    {
        spriteRenderer.enabled = false;

        if (clickSound != null)
        {
            audioSource.clip = clickSound;
            audioSource.Play();
        }
        if (removeSound != null)
        {
            audioSource.clip = removeSound;
            audioSource.Play();
        }

        if (removeEffect != null)
        {
            ParticleSystem effect = Instantiate(removeEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration); 
        }

        StartCoroutine(DeactivateAfterSound());
    }

    private System.Collections.IEnumerator DeactivateAfterSound()
    {
  //      yield return new WaitForSeconds(audioSource.clip != null ? audioSource.clip.length : 0);
        yield return new WaitForSeconds(audioSource.clip.length);

        this.gameObject.SetActive(false);
        enabled = false;
    }


    public void PlayClickSound()
    {
        if (clickSound != null)
        {
            audioSource.clip = clickSound;
            audioSource.Play();
        }
    }

    public void PlayClickEffect()
    {
        if (clickEffect != null)
        {
            ParticleSystem effect = Instantiate(clickEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration); // Clean up the particle system
        }
    }
}
