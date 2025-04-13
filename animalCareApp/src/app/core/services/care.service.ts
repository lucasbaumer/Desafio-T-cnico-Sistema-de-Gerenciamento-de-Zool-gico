import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Care } from "../models/care.model";

@Injectable({
  providedIn: 'root'
})
export class CareService{
  private apiUrl = 'http://localhost:5000/api/cares';

  constructor(private http: HttpClient){}

  getAllCares(): Observable<Care[]>{
    return this.http.get<Care[]>(this.apiUrl);
  }

  getCareById(id: number): Observable<Care>{
    return this.http.get<Care>(`${this.apiUrl}/${id}`);
  }

  createCare(care: Care): Observable<Care>{
    return this.http.post<Care>(this.apiUrl, care);
  }

  updateCare(id: number, care: Care): Observable<Care>{
    return this.http.put<Care>(`${this.apiUrl}/${id}`, care);
  }

  deleteCare(id: number): Observable<void>{
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
