﻿using System;
using RimWorld;
using Verse;
using CombatExtended;

namespace PLA.CE
{
    class StatPart_LoadedSecondaryAmmo:StatPart
    {
		public override void TransformValue(StatRequest req, ref float val)
		{
			float num;
			if (this.TryGetValue(req, out num))
			{
				val += num;
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00022564 File Offset: 0x00020964
		public override string ExplanationPart(StatRequest req)
		{
			float val;
			return (!this.TryGetValue(req, out val)) ? null : ("CE_StatsReport_LoadedAmmo".Translate() + ": " + this.parentStat.ValueToString(val, ToStringNumberSense.Absolute, true));
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000225B8 File Offset: 0x000209B8
		public bool TryGetValue(StatRequest req, out float num)
		{
			num = 0f;
			if (req.HasThing)
			{
				CompSecondaryAmmo compAmmoUser = req.Thing.TryGetComp<CompSecondaryAmmo>();
				if (compAmmoUser != null && !compAmmoUser.IsSharedAmmo && compAmmoUser.CurrentSecondaryAmmo != null)
				{
					num = compAmmoUser.CurrentSecondaryAmmo.GetStatValueAbstract(this.parentStat, null) * (float)compAmmoUser.ScondaryMagCount;
					if (this.parentStat == CE_StatDefOf.Bulk)
					{
						num *= compAmmoUser.Props.loadedAmmoBulkFactor;
					}
				}
			}
			return num != 0f;
		}
	}
}
