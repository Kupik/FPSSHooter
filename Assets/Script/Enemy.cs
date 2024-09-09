using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damageAmount = 10f; // Cantitatea de damage aplicată
    public float raycastLength = 100f; // Lungimea raycast-ului
    public float moveSpeed = 5f; // Viteza de mișcare
    public float detectionRange = 20f; // Raza de detecție pentru jucător
    public Transform player; // Referință la jucător

    public AudioSource walkSound; // Sunetul de mers
    public AudioSource runSound; // Sunetul de sprint (dacă este cazul)
    public AudioSource attackSound; // Sunetul de atac

    private Rigidbody rb;
    private Target target; // Referință la scriptul Target

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GetComponent<Target>(); // Obține componenta Target atașată acestui inamic

        if (player == null)
        {
            Debug.LogError("Referința la jucător nu este setată!");
        }
        if (target == null)
        {
            Debug.LogError("Componenta Target nu este atașată la inamic!");
        }
    }

    void Update()
    {
        // Gestionează raycast-ul pentru a detecta și a aplica damage țintelor
        HandleRaycast();

        // Gestionează urmărirea și mișcarea inamicului
        HandleMovement();
    }

    void HandleRaycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * raycastLength, Color.red); // Vizualizează raycast-ul

        if (Physics.Raycast(ray, out hit, raycastLength))
        {
            Debug.Log("Raycast a lovit: " + hit.collider.name);
            PlayerHealth hitPlayerHealth = hit.collider.GetComponent<PlayerHealth>();
            if (hitPlayerHealth != null)
            {
                hitPlayerHealth.TakeDamage(damageAmount);
                Debug.Log("Jucător lovit! Damage aplicat.");

                // Redă sunetul de atac
                if (attackSound != null && !attackSound.isPlaying)
                {
                    attackSound.Play();
                }
            }
        }
    }

    void HandleMovement()
    {
        // Verifică dacă jucătorul este în raza de detecție
        if (player != null && Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            // Calculează direcția către jucător
            Vector3 direction = (player.position - transform.position).normalized;

            // Mișcă inamicul către jucător
            rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);

            // Face inamicul să fie întotdeauna cu fața către jucător
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // 5f este viteza de rotație

            // Redă sunetul de mers sau sprint
            if (moveSpeed > 5f) // Poți ajusta această condiție pentru a diferenția între mers și sprint
            {
                if (runSound != null && !runSound.isPlaying)
                {
                    runSound.Play();
                }
                if (walkSound != null && walkSound.isPlaying)
                {
                    walkSound.Stop();
                }
            }
            else
            {
                if (walkSound != null && !walkSound.isPlaying)
                {
                    walkSound.Play();
                }
                if (runSound != null && runSound.isPlaying)
                {
                    runSound.Stop();
                }
            }
        }
        else
        {
            // Oprește sunetele dacă inamicul nu se mișcă
            if (walkSound != null && walkSound.isPlaying)
            {
                walkSound.Stop();
            }
            if (runSound != null && runSound.isPlaying)
            {
                runSound.Stop();
            }
        }
    }
}
