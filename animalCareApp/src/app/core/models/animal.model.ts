import { Care } from "./care.model";

export interface Animal{
  id: number;
  name: string;
  description: string;
  birthDate: Date;
  species: string;
  habitat: string;
  countryOfOrigin: string;
  care: Care[];
}
