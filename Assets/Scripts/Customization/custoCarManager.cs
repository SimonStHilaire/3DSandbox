using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class custoCarManager : SceneSingleton<custoCarManager>
{

    public List<VehiculeManager> cars;
    private List<UiItemDTO> partsTypes;
    List<UiItemDTO> uiParts;

    public List<AttachmentPart> parts;

    public UiItemsController uiItemsController;

    UiItemDTO CustomizeBtn;
    UiItemDTO BackBtn;

    // Start is called before the first frame update
    void Start()
    {
        AssetBundleManager.Instance.OnDownloadComplete += OnDownloadComplete;
        AssetBundleManager.Instance.Initialize();

        CustomizeBtn = new UiItemDTO { displayName = "Customize", id = Guid.NewGuid() };
        BackBtn = new UiItemDTO { displayName = "Back", id = Guid.NewGuid() };
        displayCarList();
    }

    void OnDownloadComplete()
    {
        Debug.Log("OnDownloadComplete");
        parts = AssetBundleManager.Instance.GetAllParts();
        parts.ForEach(part => part.gameObject.SetActive(false));
    }

    public void btnClick(Guid id)
    {
        // Debug.Log("btn click " + id);
        if (id == BackBtn.id)
            backBtnClick();

        else if (cars.Any(cars => cars.Id == id))
            changeCarBtnClick(id);

        else if (id == CustomizeBtn.id)
            CustomizeBtnClick();

        else if (partsTypes.Any(part => part.id == id))
            partTypeClick(id);

        else if (uiParts.Any(part => part.id == id))
            specificPartConstomize(id);
    }

    private void changeCarBtnClick(Guid carId)
    {
        foreach (VehiculeManager v in cars)
        {
           // Debug.Log("car id : " + v.Id);
            if (v.Id == carId)
                v.gameObject.SetActive(true);
            else
                v.gameObject.SetActive(false);
        }
    }

    private void CustomizeBtnClick()
    {
        partsTypes = new List<UiItemDTO>();

        foreach (string typeName in Enum.GetNames(typeof(AttachmentType)))
        {
            partsTypes.Add(new UiItemDTO { displayName = typeName, id = Guid.NewGuid() });
        }

        partsTypes.Add(BackBtn);

        uiItemsController.displayItems(partsTypes);
    }

    private void partTypeClick(Guid id)
    {
        UiItemDTO clickedPartType = partsTypes.First(part => part.id == id);

        AttachmentType type;

        Enum.TryParse(clickedPartType.displayName, out type);

        List<AttachmentPart> attachmentPartsWithType = parts.FindAll(part => part.type == type);

        uiParts = new List<UiItemDTO>();

        foreach (AttachmentPart attachmentPart in attachmentPartsWithType)
        {
            uiParts.Add(new UiItemDTO { displayName = attachmentPart.Title, id = Guid.NewGuid(), thumbnail = attachmentPart.Thumbnail });
        }

        uiParts.Add(BackBtn);

        uiItemsController.displayItems(uiParts);

    }

    private void specificPartConstomize(Guid id)
    {
        UiItemDTO selectedUiPart = uiParts.First(ui => ui.id == id);

        AttachmentPart selectedPart = parts.First(part => part.Title == selectedUiPart.displayName);

        VehiculeManager activeCar = cars.First(car => car.isActiveAndEnabled);

        activeCar.SetAttachment(selectedPart);
    }

    private void backBtnClick()
    {
        displayCarList();
    }

    private void displayCarList()
    {
        List<UiItemDTO> carsBtns = new List<UiItemDTO>();

        foreach (VehiculeManager v in cars)
        {
            v.Id = Guid.NewGuid();
            carsBtns.Add(new UiItemDTO { displayName = v.car_name, id = v.Id });
        }

        carsBtns.Add(CustomizeBtn);

        uiItemsController.displayItems(carsBtns);
    }
}
