import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TareaService {

  private myAppUrl = 'https://localhost:7166/';
  private myApiUrl = 'Tareas/';

  constructor(private http: HttpClient) { }

  getListaTareas(): Observable<any> {
    return this.http.get(this.myAppUrl + this.myApiUrl+'Listado');
  }

  deleteTarea(id: number): Observable<any> {
    return this.http.delete(this.myAppUrl + this.myApiUrl + 'Eliminar?numero=' + id); 
  }

  saveTarea(tarea: any): Observable<any> {
    return this.http.post(this.myAppUrl + this.myApiUrl + 'Agregar', tarea);
  }

  updateTarea(id: number, tarea: any): Observable<any> {
    return this.http.put(this.myAppUrl + this.myApiUrl + 'Modificar', tarea);
  }

}
