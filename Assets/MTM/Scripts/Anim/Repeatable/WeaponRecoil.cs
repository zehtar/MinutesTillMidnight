
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [Header("Base Recoil")]
    [SerializeField] private float recoilDistance = 0.15f;
    [SerializeField] private float recoilRotation = 8f;

    [Header("Movement")]
    [SerializeField] private float kickSpeed = 20f;
    [SerializeField] private float returnSpeed = 10f;

    [Header("Maximum Recoil")]
    [SerializeField] private float maxPositionOffset = 0.5f;
    [SerializeField] private float maxRotationOffset = 35f;

    [Header("Stat Influence")]
    [SerializeField] private float bulletsInfluence = 0.15f;
    [SerializeField] private float spreadInfluence = 0.02f;
    [SerializeField] private float fireRateInfluence = 0.1f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private Vector3 currentPositionOffset;
    private float currentRotationOffset;

    private Vector3 targetPositionOffset;
    private float targetRotationOffset;

    private void Awake()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    private void Update()
    {
        currentPositionOffset = Vector3.Lerp(
            currentPositionOffset,
            targetPositionOffset,
            kickSpeed * Time.deltaTime
        );

        currentRotationOffset = Mathf.Lerp(
            currentRotationOffset,
            targetRotationOffset,
            kickSpeed * Time.deltaTime
        );

        targetPositionOffset = Vector3.Lerp(
            targetPositionOffset,
            Vector3.zero,
            returnSpeed * Time.deltaTime
        );

        targetRotationOffset = Mathf.Lerp(
            targetRotationOffset,
            0f,
            returnSpeed * Time.deltaTime
        );

        transform.localPosition = initialPosition + currentPositionOffset;

        transform.localRotation = initialRotation * Quaternion.Euler(
            0f,
            0f,
            currentRotationOffset
        );
    }

    public void ApplyRecoil(int bulletsPerShot, float spread, float fireRate)
    {
        float bulletMultiplier =
            1f + (bulletsPerShot - 1) * bulletsInfluence;

        float spreadMultiplier =
            1f + spread * spreadInfluence;

        float fireRateMultiplier =
            1f + (1f / Mathf.Max(fireRate, 0.01f)) * fireRateInfluence;

        float finalMultiplier =
            bulletMultiplier * spreadMultiplier * fireRateMultiplier;

        Vector3 recoilImpulse =
            Vector3.left * recoilDistance * finalMultiplier;

        float rotationImpulse =
            recoilRotation * finalMultiplier;

        targetPositionOffset += recoilImpulse;
        targetRotationOffset += rotationImpulse;

        targetPositionOffset = Vector3.ClampMagnitude(
            targetPositionOffset,
            maxPositionOffset
        );

        targetRotationOffset = Mathf.Clamp(
            targetRotationOffset,
            -maxRotationOffset,
            maxRotationOffset
        );
    }
}
