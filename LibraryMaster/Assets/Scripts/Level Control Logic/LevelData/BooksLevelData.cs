﻿using System;
using System.Linq;
using BookLogic;
using UnityEngine;

namespace ATG.LevelControl
{
    [Serializable]
    public class BooksData
    {
        [SerializeField] private Book[] _books;

        public Book[] Books => _books;
    }
    
    public class BooksLevelData: LevelData
    {
        [Space(10)]
        [SerializeField] private LineLevelType _levelType;
        [Space(10)]
        [SerializeField] private ShelfBlock[] _shelfs;
        [SerializeField] private BooksData[] _booksOnShelfs;
        
        public override LevelType TypeOfLevel => (LevelType) Enum.Parse(typeof(LevelType), _levelType.ToString());
        
        public override T[] GetAnsInstantiateLevelBlocks<T>(ICreateLevelBehaviour createLevel)
        {
            if (createLevel == null)
                throw new NullReferenceException("CreateLevelBehaviour is null");
            
            var levelParent = CreateSceneDataObjects();
            
            var result = createLevel.InstantiateBlocks<T,ShelfBlock>(_shelfs, levelParent);

            var spawnedShelfs = result.Cast<ShelfBlock>().ToArray();
            if (spawnedShelfs.Length > 0 && _booksOnShelfs.Length >= spawnedShelfs.Length)
            {
                for (var i = 0; i < spawnedShelfs.Length; i++)
                {
                    spawnedShelfs[i].InitBooks(_booksOnShelfs[i].Books);
                }
            }
            else
            {
                throw new IndexOutOfRangeException("Books array length is less than shelfs length");
            }

            return result;
        }
    }
}