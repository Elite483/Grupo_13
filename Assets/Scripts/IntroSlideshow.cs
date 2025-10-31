using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class IntroSlideshow : MonoBehaviour
{
    public Image displayImage;        // La imagen del UI
    public Sprite[] slides;           // Lista de imágenes
    public float slideDuration = 8f;  // Tiempo que la imagen se mantiene
    public float fadeDuration = 1f;   // Tiempo del fade in/out
    public string nextSceneName;      // Escena a cargar al finalizar

    private int currentSlide = 0;
    private CanvasGroup canvasGroup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Agregar CanvasGroup si no existe
        canvasGroup = displayImage.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = displayImage.gameObject.AddComponent<CanvasGroup>();

        if (slides.Length > 0)
        {
            StartCoroutine(ShowSlides());
        }
    }

    System.Collections.IEnumerator ShowSlides()
    {
        while (currentSlide < slides.Length)
        {
            displayImage.sprite = slides[currentSlide];

            // Fade in
            yield return StartCoroutine(Fade(0f, 1f));

            // Esperar mientras la imagen se muestra
            yield return new WaitForSeconds(slideDuration - 2 * fadeDuration);

            // Fade out
            yield return StartCoroutine(Fade(1f, 0f));

            currentSlide++;
        }

        // Al terminar, cargar la siguiente escena
        SceneManager.LoadScene(nextSceneName);
    }

    System.Collections.IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            canvasGroup.alpha = alpha;
            yield return null;
        }

        canvasGroup.alpha = endAlpha; // Aseguramos el valor final
    }
}

