using System;
using System.Collections.Generic;

public class ParkingManager : Singleton<ParkingManager> {

    public event EventHandler OnParkingPointOccupied;
    //public event EventHandler OnParkingPointUnoccupied;

    private List<ParkingPoint> _occupiedParkingPoint = new List<ParkingPoint>();


    public void AddParkingPointToOccupyList(ParkingPoint _parkingPoint) {
        _occupiedParkingPoint.Add(_parkingPoint);
        OnParkingPointOccupied?.Invoke(this, EventArgs.Empty);
    }
    public void RemoveParkingPointFromOccupyList(ParkingPoint _parkingPoint) {
        _occupiedParkingPoint.Remove(_parkingPoint);
    }
}
