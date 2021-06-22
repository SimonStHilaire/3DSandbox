using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiculeManager : MonoBehaviour
{

    public Guid Id { get; set; }

    public string car_name;

    public Sprite Thumbnail;

    public AttachmentPoint RoofAttachmentPoint;
    public AttachmentPoint BulbarAttachmentPoint;
    public AttachmentPoint HeadlightAttachmentPoint;
    public List<AttachmentPoint> TireAttachmentPoints;//FR, FL, BR, BL

    GameObject RoofPart = null;
    GameObject BulbarPart = null;
    GameObject HeadlightPart = null;
    GameObject[] TireParts = null;

    public float RotationSpeed = 10;


    private void OnMouseDrag()
    {
        transform.Rotate(0, (Input.GetAxis("Mouse X") * RotationSpeed * -1), 0, Space.World);
    }

    public void SetAttachment(AttachmentPart part)
    {
        switch(part.type)
        {
            case AttachmentType.Roof:
                {
                    if(RoofPart != null)
                    {
                        Destroy(RoofPart.gameObject);
                    }

                    RoofPart = Instantiate(part.gameObject);

                    RoofPart.transform.SetParent(RoofAttachmentPoint.transform, false);
                    RoofPart.gameObject.SetActive(true);

                    break;
                }
            case AttachmentType.Bulbar:
                {
                    if (BulbarPart != null)
                    {
                        Destroy(BulbarPart.gameObject);
                    }

                    BulbarPart = Instantiate(part.gameObject);

                    BulbarPart.transform.SetParent(BulbarAttachmentPoint.transform, false);
                    BulbarPart.gameObject.SetActive(true);
                    break;
                }
            case AttachmentType.Headlight:
                {
                    if (HeadlightPart != null)
                    {
                        Destroy(HeadlightPart.gameObject);
                    }

                    HeadlightPart = Instantiate(part.gameObject);

                    HeadlightPart.transform.SetParent(HeadlightAttachmentPoint.transform, false);
                    HeadlightPart.gameObject.SetActive(true);
                    break;
                }
            case AttachmentType.Tire:
                {
                    if(TireParts != null)
                    {
                        foreach (GameObject tire in TireParts)
                            Destroy(tire);
                    }
                    else
                    {
                        TireParts = new GameObject[4];
                    }

                    for (int i = 0; i < 4; ++i)
                    {
                        TireParts[i] = Instantiate(part.gameObject);
                        TireParts[i].transform.SetParent(TireAttachmentPoints[i].transform, false);
                        TireParts[i].SetActive(true);
                    }

                    break;
                }
        }
    }
}
