﻿using System.Collections.Generic;
using UnityEngine;

namespace Game.Tasks.EnergyCore
{
    public class ManageEnergyCore : MonoBehaviour
    {
        public GameObject energyCoreRed;
        public GameObject energyCoreBlue;
        public GameObject energyCoreGreen;
        public GameObject energyCoreYellow;
        public GameObject energyCoreGray;
        public GameObject energyCoreMagenta;
        public GameObject energyCoreCyan;

        public GameObject energyCell1;
        public GameObject energyCell2;
        public GameObject energyCell3;
        public GameObject energyCell4;
        public GameObject energyCell5;
        public GameObject energyCell6;
        public GameObject energyCell7;
        
        private void Start()
        {
            SetColor(energyCoreRed, Color.red);
            SetColor(energyCoreBlue, Color.blue);
            SetColor(energyCoreYellow, Color.yellow);
            SetColor(energyCoreGreen, Color.green);
            SetColor(energyCoreGray, Color.gray);
            SetColor(energyCoreMagenta, Color.magenta);
            SetColor(energyCoreCyan, Color.cyan);
            
            var colors = new List<Color>
            {
                Color.red,
                Color.green,
                Color.blue,
                Color.yellow,
                Color.cyan,
                Color.magenta,
                Color.gray
            };

            SetColor(energyCell1, SelectRandomColor(colors));
            SetColor(energyCell2, SelectRandomColor(colors));
            SetColor(energyCell3, SelectRandomColor(colors));
            SetColor(energyCell4, SelectRandomColor(colors));
            SetColor(energyCell5, SelectRandomColor(colors));
            SetColor(energyCell6, SelectRandomColor(colors));
            SetColor(energyCell7, SelectRandomColor(colors));
        }


        private static void SetColor(GameObject target, Color newColor)
        {
            var material = target.GetComponent<Renderer>().material;
            material.color = newColor;
        }

        private static Color SelectRandomColor(IList<Color> possibleColors)
        {
            var randomIndex = Random.Range(0, possibleColors.Count);

            var selectedColor = possibleColors[randomIndex];
            
            possibleColors.RemoveAt(randomIndex);

            return selectedColor;
        }

        public static Color GetColor(GameObject target)
        {
            return target.GetComponent<Renderer>().material.color;
        }
    }
}