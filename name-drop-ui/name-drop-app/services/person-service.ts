import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../src/environments/environment';

// Adjust fields to match your backend DTO
export interface Person {
  name: string;
  nationality: string;
  gender: string;
  age: number;
}

@Injectable({
  providedIn: 'root'
})
export class PersonService {
  private apiUrl =  environment.apiUrl + '/main/predict-person';
  private apiKey =  environment.apiKey;

  constructor(private http: HttpClient) {}

  // GET person by name
  getPerson(name: string): Observable<Person> {
    return this.http.get<Person>(`${this.apiUrl}/${name}`, { headers: { 'ApiKey': this.apiKey } });
  }
}
