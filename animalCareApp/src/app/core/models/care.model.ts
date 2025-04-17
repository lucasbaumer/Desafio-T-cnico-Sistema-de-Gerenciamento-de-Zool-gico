import { Animal } from "./animal.model";

export interface Care {
  id: string;
  careName: string;
  description: string;
  frequency: string;
  animalCares?: { animalId: string }[];
}
