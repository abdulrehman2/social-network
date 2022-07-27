import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ReactToCreate } from '../models/reactToCreate';

@Injectable({
  providedIn: 'root',
})
export class ReactService {
  private baseUrl = `${environment.apiURL}/api/social/postreact`;

  constructor(private http: HttpClient) {}

  //headers: { 'Content-Type': 'multipart/form-data' }
  addReact(reactModel: ReactToCreate): any {
    return this.http.post<any>(this.baseUrl, reactModel);
  }
}
