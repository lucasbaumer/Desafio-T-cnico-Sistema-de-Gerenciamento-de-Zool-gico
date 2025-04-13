import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Care } from "../models/care.model";
import { environment } from "../../../Environment/Environment";

@Injectable({
  providedIn: 'root'
})
export class CareService{
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient){}

  getAllCares(): Observable<Care[]>{
    return this.http.get<Care[]>(`${this.apiUrl}/care`);
  }

  getCareById(id: string): Observable<Care> {
    return this.http.get<Care>(`${this.apiUrl}/care/${id}`);
  }

  createCare(care: Care): Observable<Care>{
    return this.http.post<Care>(`${this.apiUrl}/care`, care);
  }

  updateCare(id: string, care: Care): Observable<Care> {  // Alterado para string
    return this.http.put<Care>(`${this.apiUrl}/care/${id}`, care);
  }

  deleteCare(id: string): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/care/${id}`);
  }

}
