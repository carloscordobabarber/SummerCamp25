export interface ApartmentCard {
  id: number;
  code: string;
  door: string;
  floor: string;
  price: number;
  area: number;
  numberOfRooms: number;
  numberOfBathrooms: number;
  buildingId: number;
  hasLift: boolean;
  hasGarage: boolean;
  isAvailable: boolean;
  streetName: string;
  districtId: number;
  districtName: string;
  imageUrls: string[];
}
