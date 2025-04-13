import { Care } from "./care.model";

export interface Animal{
  id: string;
  name: string;
  description: string;
  dateOfBirth: string;
  species: string;
  habitat: string;
  countryOfOrigin: string;
  care: Care[];
}
