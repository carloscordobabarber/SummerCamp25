export interface ApartmentFilters {
  page?: number;
  pageSize?: number;
  minPrice?: number;
  maxPrice?: number;
  area?: number;
  numberOfRooms?: number;
  numberOfBathrooms?: number;
  street?: string;
  code?: string;
}
