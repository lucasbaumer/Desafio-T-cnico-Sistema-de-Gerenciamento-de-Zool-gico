import { Animal } from "./animal.model";

export interface Care {
  id: number;
  name: string;
  description: string;
  frequency: string;
  animalIds: number[]
}
