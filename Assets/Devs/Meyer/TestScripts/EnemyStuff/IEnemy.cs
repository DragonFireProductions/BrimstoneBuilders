///-------------------------------------------------------------------------------------------------
// file:	Assets\Meyer\TestScripts\IEnemy.cs
//
// summary:	Declares the IEnemy interface

using System.Collections.Generic;
using UnityEngine;

namespace Kristal
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary> Interface for enemy. </summary>
    ///
    /// <remarks> Simpl, 9/25/2018. </remarks>

    public interface IEnemy
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary> Damages. </summary>
        ///
        /// <param name="_damage"> The damage. </param>

        void Damage(int _damage);

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets the enemies. </summary>
        ///
        /// <returns> The enemies. </returns>

        List<GameObject> GetEnemies();

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets the health. </summary>
        ///
        /// <returns> The health. </returns>

        int GetHealth();
    }
}