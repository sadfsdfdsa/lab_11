﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace lab_10
{
    public interface IPrint
    {
        void Print();
    }

    public class Production : IComparable, IPrint
    {
        private int _workersNumber;

        public int WorkersNumber
        {
            get => _workersNumber;
            set => _workersNumber = value;
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"Workers: {WorkersNumber}");
        }

        // todo work only with new key
        public void ShowInfoFake()
        {
            Console.WriteLine($"Workers: {WorkersNumber}");
        }

        public int CompareTo(object obj)
        {
            Production temp = (Production) obj;
            if (WorkersNumber > temp.WorkersNumber) return 1;
            if (WorkersNumber < temp.WorkersNumber) return -1;
            return 0;
        }

        public void Print()
        {
            ShowInfo();
        }
    }

    public class Factory : Production, IPrint
    {
        private string _factoryName;

        public string FactoryName
        {
            get => _factoryName;
            set => _factoryName = value;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"Factory name: {FactoryName}, Workers: {WorkersNumber}");
        }

        // todo work only with new key
        public new void ShowInfoFake()
        {
            Console.WriteLine($"Factory name: {FactoryName}, Workers: {WorkersNumber}");
        }

        public new void Print()
        {
            Console.Write("IPrint, factory:");
            ShowInfo();
        }
    }

    public class Shop : Factory
    {
        private int _mainWorkerNumber;
        private string _shopName;

        public string ShopName
        {
            get => _shopName;
            set => _shopName = value;
        }

        public int MainWorkerNumber
        {
            get => _mainWorkerNumber;
            set => _mainWorkerNumber = value;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"Engineer number: {MainWorkerNumber}, Workers: {WorkersNumber}");
        }
    }

    public class Workshop : Production, ICloneable, IPrint
    {
        private int _managersNumber;

        public int ManagersNumber
        {
            get => _managersNumber;
            set => _managersNumber = value;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"Managers: {ManagersNumber}, Workers: {WorkersNumber}");
        }

        public object Clone()
        {
            return new Workshop {ManagersNumber = ManagersNumber, WorkersNumber = WorkersNumber};
        }

        public new void Print()
        {
            Console.Write("IPrint, workshop: ");
            ShowInfo();
        }
    }


    // abstract
    public abstract class PlantAbstract
    {
        private Shop[] _shops;
        public abstract void Show();
    }

    public class Plant : PlantAbstract
    {
        private Shop[] _shops;

        public Shop[] Shops
        {
            get => _shops;
            set => _shops = value;
        }

        public override void Show()
        {
            foreach (Shop shop in _shops)
            {
                shop.ShowInfo();
            }
        }
    }

    // sorting
    public class SortByWorkersNumber : IComparer
    {
        int IComparer.Compare(object ob1, object ob2)
        {
            Production s1 = (Production) ob1;
            Production s2 = (Production) ob2;
            if (s1.WorkersNumber == s2.WorkersNumber)
            {
                return 0;
            }

            return s1.WorkersNumber > s2.WorkersNumber ? 1 : -1;
        }
    }
}