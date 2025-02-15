using UnityEngine;

public class MovingBG : MonoBehaviour
{
    [SerializeField]
    private float _scrollSpeed = 1f; // Speed of the scroll
    [SerializeField]
    private float _textureHeight = 19.20f; // Height of each texture

    private Transform[] backgrounds; // Array to hold the two background textures

    void Start()
    {
        // Initialize the backgrounds array with the two child objects
        backgrounds = new Transform[2];
        backgrounds[0] = transform.GetChild(0);
        backgrounds[1] = transform.GetChild(1);

        // Position the second texture above the first
        backgrounds[1].position = new Vector3(0, _textureHeight, 0);
    }

    void Update()
    {
        // Scroll both textures down
        foreach (Transform background in backgrounds)
        {
            background.Translate(Vector3.down * _scrollSpeed * Time.deltaTime);

            // Check if the texture has moved below the screen
            if (background.position.y <= -_textureHeight)
            {
                // Move the texture to the top
                background.position = new Vector3(0, _textureHeight, 0);
            }
        }
    }
}
