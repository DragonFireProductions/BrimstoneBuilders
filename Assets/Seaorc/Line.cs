using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] Transform Base;
    [SerializeField] List<Transform> transforms;
    [SerializeField] float Spacing;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            FormLine();
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            SetActive(true);
        }
    }

    public void SetActive(bool _Active)
    {
        Base.gameObject.SetActive(_Active);

        foreach (Transform item in transforms)
        {
            item.gameObject.SetActive(_Active);
        }
    }

    public void FormLine()
    {
        int Half = (int)(transforms.Count * .5f);


        for (int i = 0; i < Half; i++)
        {
            transforms[i].position = Base.position;
            transforms[i].rotation = Base.rotation;
            transforms[i].Translate(Spacing * (i + 1), 0, 0);
        }

        for (int i = Half; i < transforms.Count; i++)
        {
            transforms[i].position = Base.position;
            transforms[i].rotation = Base.rotation;
            transforms[i].Translate(-Spacing * ((i + 1) - Half), 0, 0);
        }
    }

    public void SetBase(Transform _NewBase)
    {
        Base = _NewBase;
    }

    public void SetSpacing(float _Distance)
    {
        Spacing = _Distance;
    }
}