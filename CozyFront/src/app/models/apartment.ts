export interface Apartment {
    id: number,
    code: string,
    door: string,
    floor: number,
    price: number,
    area: number,
    numberOfRooms: number,
    numberOfBathrooms: number,
    buildingId: number,
    hasLift: boolean,
    hasGarage: boolean,
    isAvailable: boolean,
    imageUrls?: string[],
    districtName: string,
    streetName: string
}
