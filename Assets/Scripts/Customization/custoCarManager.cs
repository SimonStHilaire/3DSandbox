using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class custoCarManager : SceneSingleton<custoCarManager>
{

    public List<VehiculeManager> cars;

    public UiItemsController uiItemsController;

    private List<UiItemDTO> carsBtns;

    private int lastId = 0;

    // Start is called before the first frame update
    void Start()
    {
        carsBtns = new List<UiItemDTO>();

        foreach (VehiculeManager v in cars) {
            carsBtns.Add(new UiItemDTO { displayName = v.car_name, id = lastId++ } );
        }

        uiItemsController.displayItems(carsBtns);
    }

    private void instantiateCar(VehiculeManager car)
    {
        car.gameObject.SetActive(true);
    }

    public void btnClick(int id)
    {
        Debug.Log("btn click " + id);
        //carsBtns.Find(cars => cars.id == id);
    }
}
