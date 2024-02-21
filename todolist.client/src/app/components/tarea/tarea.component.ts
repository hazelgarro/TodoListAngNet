import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TareaService } from '../../services/tarea.service';

@Component({
  selector: 'app-tarea',
  templateUrl: './tarea.component.html',
  styleUrl: './tarea.component.css'
})
export class TareaComponent implements OnInit{

  listaTareas: any[] = [];
  accion = 'Agregar';
  id: number | undefined;

  form: FormGroup;

  constructor(private fb: FormBuilder,
    private _tareaService: TareaService) {
    this.form = this.fb.group({
      id: ['', [Validators.required, Validators.minLength(1)] ],
      nombre: ['', [Validators.required, Validators.maxLength(30)], Validators.minLength(1) ], //como son varias validaciones va en un array
      descripcion: ['', [Validators.required, Validators.maxLength(50)], Validators.minLength(1)],
      estado: ['', [Validators.required, Validators.maxLength(1)], Validators.minLength(1)],
    })
  }

  ngOnInit(): void {
    this.obtenerTareas();
  }

  obtenerTareas() {
    this._tareaService.getListaTareas().subscribe(data => {
      console.log(data);
      this.listaTareas = data; 
    }, error => {
      console.log(error);
    })
  }


  guardarTarea() {
    console.log(this.form);

    const tarea: any = {
      id: this.form.get('id')?.value,
      nombre: this.form.get('nombre')?.value,
      descripcion: this.form.get('descripcion')?.value,
      estado: this.form.get('estado')?.value,
    }

    if (this.id == undefined) {
      //se agrega una nueva tarea
      this._tareaService.saveTarea(tarea).subscribe(data => {
        console.log(tarea);
        this.form.reset();
        this.obtenerTareas();
      }, error => {
        console.log(error);
      })
    }
    else//si no se guarda la modificacion
    {
      tarea.id = this.id;
      this._tareaService.updateTarea(this.id, tarea).subscribe(data => {
        this.form.reset();
        this.accion = "Agregar";
        this.id = undefined;
        this.obtenerTareas();
      }, error => {
        console.log(error);
      })
    }

    
  }

  eliminarTarea(id: number) {
    this._tareaService.deleteTarea(id).subscribe(data => {
      console.log("eliminado" + id);
      this.obtenerTareas();
    }, error => {
      console.log(error);
    })
   
  }

  editarTarea(tarea: any) {
    this.accion = "Editar"
    console.log(tarea);
    this.id = tarea.id;
    this.form.patchValue({
      id: tarea.id,
      nombre: tarea.nombre,
      descripcion: tarea.descripcion,
      estado: tarea.estado,
    })
    this.obtenerTareas();
  }

}
