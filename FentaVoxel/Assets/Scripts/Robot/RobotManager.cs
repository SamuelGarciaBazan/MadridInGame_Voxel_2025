using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gestiona la colección de robots en escena y su asignación a recursos o libres.
/// </summary>
public class RobotManager : MonoBehaviour
{
    public LayerMask _woodLayer;
    public LayerMask _waterLayer;
    public LayerMask _ironLayer;
    public LayerMask _copperLayer;

    public enum RobotResourceTarget
    {
        NONE,
        GET_WOOD,
        GET_WATER,
        GET_IRON,
        GET_COPPER
    }

    [Header("Robot Prefab")]
    [SerializeField] private GameObject robotPrefab;
    [SerializeField] private SpendingResourceManager spendingResourceManager;

    // Listas de robots según estado
    private readonly List<GameObject> freeRobots = new List<GameObject>();
    private readonly List<GameObject> woodRobots = new List<GameObject>();
    private readonly List<GameObject> waterRobots = new List<GameObject>();
    private readonly List<GameObject> ironRobots = new List<GameObject>();

    /// <summary>
    /// Evento disparado cuando cambia la cantidad de robots en cualquier lista.
    /// </summary>
    public event Action OnRobotCountsChanged;

    /// <summary>
    /// Número de robots libres.
    /// </summary>
    public int FreeRobots => freeRobots.Count;

    /// <summary>
    /// Número total de robots trabajando (no incluye libres).
    /// </summary>
    public int WorkingRobotsAmount() {
        return woodRobots.Count + waterRobots.Count + ironRobots.Count;
    }

    /// <summary>
    /// Devuelve la cantidad de robots asignados al recurso indicado.
    /// </summary>
    public int GetRobots(ResourcesManager.ResourcesType type) {
        return type switch {
            ResourcesManager.ResourcesType.WOOD => woodRobots.Count,
            ResourcesManager.ResourcesType.WATER => waterRobots.Count,
            ResourcesManager.ResourcesType.IRON => ironRobots.Count,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    /// <summary>
    /// Asigna o desasigna robots entre lista libre y lista de recurso.
    /// +1 para asignar libre->recurso, -1 para recurso->libre.
    /// </summary>
    public bool AssignRobots(ResourcesManager.ResourcesType type, int delta) {
        if (delta > 0)
            return MoveFromFree(type);
        else if (delta < 0)
            return MoveToFree(type);
        return false;
    }

    /// <summary>
    /// Crea un nuevo robot (se añade a libres y queda inactivo).
    /// </summary>
    public GameObject CreateRobot(Vector3 position = default) {
        var go = Instantiate(robotPrefab, position, Quaternion.identity);
        freeRobots.Add(go);
        go.SetActive(false);
        OnRobotCountsChanged?.Invoke();
        spendingResourceManager.SetRobotsAmount(WorkingRobotsAmount());
        return go;
    }

    /// <summary>
    /// Envía todos los robots trabajando de vuelta a libres.
    /// </summary>
    public void Apagon() {
        // Mover todos los robots de madera, agua e hierro a libres
        MoveAllToFree(woodRobots);
        MoveAllToFree(waterRobots);
        MoveAllToFree(ironRobots);
        OnRobotCountsChanged?.Invoke();
        spendingResourceManager.SetRobotsAmount(WorkingRobotsAmount());
    }

    /// <summary>
    /// Mueve un robot de un recurso a la lista de libres.
    /// </summary>
    public bool MoveToFree(ResourcesManager.ResourcesType type) {
        var list = GetListByType(type);
        if (list.Count == 0)
            return false;
        var robot = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
        freeRobots.Add(robot);
        robot.GetComponent<RobotController>().setResourceTarget(RobotResourceTarget.NONE);
        OnRobotCountsChanged?.Invoke();
        spendingResourceManager.SetRobotsAmount(WorkingRobotsAmount());
        return true;
    }

    /// <summary>
    /// Mueve un robot de libres al recurso especificado.
    /// </summary>
    public bool MoveFromFree(ResourcesManager.ResourcesType type) {
        if (freeRobots.Count == 0)
            return false;
        var robot = freeRobots[freeRobots.Count - 1];
        freeRobots.RemoveAt(freeRobots.Count - 1);
        var list = GetListByType(type);
        list.Add(robot);
        robot.SetActive(true);
        robot.GetComponent<RobotController>().setResourceTarget(ConvertToResourceTarget(type));
        OnRobotCountsChanged?.Invoke();
        spendingResourceManager.SetRobotsAmount(WorkingRobotsAmount());
        return true;
    }

    /// <summary>
    /// Método auxiliar para mover todos los robots de una lista a libres.
    /// </summary>
    private void MoveAllToFree(List<GameObject> list) {
        foreach (var robot in list) {
            freeRobots.Add(robot);
            robot.GetComponent<RobotController>().setResourceTarget(RobotResourceTarget.NONE);
            robot.SetActive(false);
        }
        list.Clear();
    }

    /// <summary>
    /// Obtiene la lista interna correspondiente al tipo de recurso.
    /// </summary>
    private List<GameObject> GetListByType(ResourcesManager.ResourcesType type) {
        return type switch {
            ResourcesManager.ResourcesType.WOOD => woodRobots,
            ResourcesManager.ResourcesType.WATER => waterRobots,
            ResourcesManager.ResourcesType.IRON => ironRobots,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    /// <summary>
    /// Convierte de RobotResourceTarget a ResourcesType.
    /// </summary>
    public static ResourcesManager.ResourcesType ConvertToResourceType(RobotResourceTarget target) {
        return target switch {
            RobotResourceTarget.GET_WOOD => ResourcesManager.ResourcesType.WOOD,
            RobotResourceTarget.GET_WATER => ResourcesManager.ResourcesType.WATER,
            RobotResourceTarget.GET_IRON => ResourcesManager.ResourcesType.IRON,
            RobotResourceTarget.GET_COPPER => ResourcesManager.ResourcesType.COPPER,
            _ => throw new ArgumentException($"RobotResourceTarget inválido: {target}")
        };
    }

    /// <summary>
    /// Convierte de ResourcesType a RobotResourceTarget.
    /// </summary>
    public static RobotResourceTarget ConvertToResourceTarget(ResourcesManager.ResourcesType type) {
        return type switch {
            ResourcesManager.ResourcesType.WOOD => RobotResourceTarget.GET_WOOD,
            ResourcesManager.ResourcesType.WATER => RobotResourceTarget.GET_WATER,
            ResourcesManager.ResourcesType.IRON => RobotResourceTarget.GET_IRON,
            ResourcesManager.ResourcesType.COPPER => RobotResourceTarget.GET_COPPER,
            _ => throw new ArgumentException($"ResourcesType inválido: {type}")
        };
    }
}
