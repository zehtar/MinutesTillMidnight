using UnityEngine;

public class DamageTextParticle : MonoBehaviour
{
    public ParticleSystem textParticleSystem; // Ссылка на Particle System

    public void ShowDamage(int damage, Vector3 position)
    {
        Debug.Log(damage);
        transform.position = position;

        // Разбиваем урон на отдельные цифры
        string damageStr = damage.ToString();

        // Запускаем частицы для каждой цифры
        foreach (char digit in damageStr)
        {
            int num = digit - '0'; // Преобразуем символ в число

            // Создаём эмиттер частицы
            var emitParams = new ParticleSystem.EmitParams();
            emitParams.startColor = Color.white;
            emitParams.startSize = 1f;
            emitParams.position = position + (Vector3)Random.insideUnitCircle * 0.2f; // Немного разбрасываем цифры
            emitParams.rotation = Random.Range(-15f, 15f); // Лёгкий поворот
            emitParams.startLifetime = 1f; // Сколько живёт текст
            emitParams.startSize = 0.5f;

            // Выбираем кадр из текстуры (по цифре)
            emitParams.startLifetime = 1.2f;
            emitParams.startSize3D = new Vector3(num, 0, 0); // Номер кадра

            // Создаём частицу
            textParticleSystem.Emit(emitParams, 1);
        }
    }
}
