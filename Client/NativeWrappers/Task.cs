using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace FivemToolsLib.Client.NativeWrappers
{
    /// <summary>
    /// Provides wrapper methods for assigning advanced AI tasks to NPCs (peds).
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Assigns a heli mission to a pilot NPC with the specified parameters.
        /// </summary>
        public static void TaskHeliMission(
              int pilot,
              int aircraft,
              int targetVehicle,
              int targetPed,
              float destinationX,
              float destinationY,
              float destinationZ,
              int missionFlag,
              float maxSpeed)
        {
            Function.Call(
                Hash.TASK_HELI_MISSION,
                pilot,
                aircraft,
                targetVehicle,
                targetPed,
                destinationX,
                destinationY,
                destinationZ,
                missionFlag,
                maxSpeed,
                -1.0f,
                -1.0f,
                80,
                -1.0f,
                -1.0f,
                8192
            );
        }

        /// <summary>
        /// Internal method for assigning a vehicle mission to drive to specific coordinates.
        /// </summary>
        private static void TaskVehicleMissionCoorsTargetInternal(
            int pedId,
            int vehicleId,
            float posX,
            float posY,
            float posZ,
            int missionFlag,
            float maxSpeed,
            int drivingStyle,
            float radius,
            float straightLineDist,
            bool driveAgainstTraffic
            )
        {
            Function.Call(
                Hash.TASK_VEHICLE_MISSION_COORS_TARGET,
                pedId, 
                vehicleId, 
                posX, 
                posY, 
                posZ, 
                missionFlag, 
                maxSpeed, 
                drivingStyle, 
                radius, 
                straightLineDist, 
                driveAgainstTraffic
            );
        }

        /// <summary>
        /// Assigns a vehicle mission for a ped to drive to specific coordinates.
        /// </summary>
        public static void TaskVehicleMissionCoorsTarget(
            int pedId,
            int vehicleId,
            float posX,
            float posY,
            float posZ,
            int missionFlag,
            float maxSpeed,
            int drivingStyle,
            float radius,
            float straightLineDist,
            bool driveAgainstTraffic
        ) => TaskVehicleMissionCoorsTargetInternal(pedId, vehicleId, posX, posY, posZ, missionFlag, maxSpeed,
            drivingStyle, radius, straightLineDist, driveAgainstTraffic);

        /// <summary>
        /// Assigns a vehicle mission for a ped to drive to specific coordinates using a <see cref="Vector3"/>.
        /// </summary>
        public static void TaskVehicleMissionCoorsTarget(
            int pedId,
            int vehicleId,
            Vector3 position,
            int missionFlag,
            float maxSpeed,
            int drivingStyle,
            float radius,
            float straightLineDist,
            bool driveAgainstTraffic
        ) => TaskVehicleMissionCoorsTargetInternal(pedId, vehicleId, position.X, position.Y, position.Z, missionFlag, maxSpeed,
            drivingStyle, radius, straightLineDist, driveAgainstTraffic);
        
        /// <summary>
        /// Assigns a vehicle mission for a <see cref="CitizenFX.Core.Ped"/> to drive to specific coordinates.
        /// </summary>
        public static void TaskVehicleMissionCoorsTarget(
            CitizenFX.Core.Ped pedId,
            Vehicle vehicleId,
            float posX,
            float posY,
            float posZ,
            int missionFlag,
            float maxSpeed,
            int drivingStyle,
            float radius,
            float straightLineDist,
            bool driveAgainstTraffic
        ) => TaskVehicleMissionCoorsTargetInternal(pedId.Handle, vehicleId.Handle, posX, posY, posZ, missionFlag, maxSpeed,
            drivingStyle, radius, straightLineDist, driveAgainstTraffic);
        
        /// <summary>
        /// Assigns a vehicle mission for a <see cref="CitizenFX.Core.Ped"/> to drive to specific coordinates using a <see cref="Vector3"/>.
        /// </summary>
        public static void TaskVehicleMissionCoorsTarget(
            CitizenFX.Core.Ped pedId,
            Vehicle vehicleId,
            Vector3 position,
            int missionFlag,
            float maxSpeed,
            int drivingStyle,
            float radius,
            float straightLineDist,
            bool driveAgainstTraffic
        ) => TaskVehicleMissionCoorsTargetInternal(pedId.Handle, vehicleId.Handle, position.X, position.Y, position.Z, missionFlag, maxSpeed,
            drivingStyle, radius, straightLineDist, driveAgainstTraffic);
    }
}