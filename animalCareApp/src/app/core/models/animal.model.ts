import { Care } from "./care.model";

export interface Animal{
  id: string;
  name: string;
  description: string;
  birthDate: string | Date;
  species: string;
  habitat: string;
  countryOfOrigin: string;
  care?: any[];
}
