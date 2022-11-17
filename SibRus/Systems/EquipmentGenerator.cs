using SibRus.Equipment;

namespace SibRus.Systems
{
    public class EquipmentGenerator
    {
        private readonly Pool<Core.Equipment> _equipmentPool;

        public EquipmentGenerator(int level)
        {
            _equipmentPool = new Pool<Core.Equipment>();

            if (level <= 3)
            {
                _equipmentPool.Add(BodyEquipment.Leather(), 20);
                _equipmentPool.Add(HeadEquipment.Leather(), 20);
                _equipmentPool.Add(FeetEquipment.Leather(), 20);
                _equipmentPool.Add(HandEquipment.Shiv(), 25);
                _equipmentPool.Add(HandEquipment.Machete(), 5);
                _equipmentPool.Add(HeadEquipment.Chain(), 5);
                _equipmentPool.Add(BodyEquipment.Vest(), 5);
            }
            else if (level <= 6)
            {
                _equipmentPool.Add(BodyEquipment.Vest(), 20);
                _equipmentPool.Add(HeadEquipment.Chain(), 20);
                _equipmentPool.Add(FeetEquipment.Padded(), 20);
                _equipmentPool.Add(HandEquipment.Machete(), 15);
                _equipmentPool.Add(HandEquipment.Axe(), 15);
                _equipmentPool.Add(HeadEquipment.Plate(), 5);
                _equipmentPool.Add(BodyEquipment.Plate(), 5);
            }
            else
            {
                _equipmentPool.Add(BodyEquipment.Plate(), 25);
                _equipmentPool.Add(HeadEquipment.Plate(), 25);
                _equipmentPool.Add(FeetEquipment.Plate(), 25);
                _equipmentPool.Add(HandEquipment.DualDaggers(), 25);
            }
        }

        public Core.Equipment CreateEquipment()
        {
            return _equipmentPool.Get();
        }
    }
}
