import { Component, ViewChild, Inject } from '@angular/core';
import { MatDialog, MatTable } from '@angular/material';
import { DialogBoxComponent } from './dialog-box/dialog-box.component';
import { HttpClient } from '@angular/common/http';

export interface UsersData {
  name: string;
  id: number;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})

export class AppComponent {
  displayedColumns: string[] = ['name', 'description'];
  dataSource: any;  
  public product: Product;
  @ViewChild(MatTable,{static:true}) table: MatTable<any>;

  constructor(public dialog: MatDialog, public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
    http.get<Product[]>(baseUrl + 'api/Product').subscribe(result => {
       this.dataSource = result;
    }, error => console.error(error));
  }

  openDialog(action,obj) {
    obj.action = action;
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '300px',
      data:obj
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result.event == 'Add'){
        this.addRowData(result.data);
      }else if(result.event == 'Update'){
        this.updateRowData(result.data);
      }else if(result.event == 'Delete'){
        this.deleteRowData(result.data);
      }
    });
  }

  addRowData(newRow){  
    this.dataSource.push({      
      name:newRow.name,
      description:newRow.description
    });

    this.product = {   
       id:newRow.id  ,
       name:newRow.name,
       description:newRow.description
    };
    const headers = { 'content-type': 'application/json'}  
    const body=JSON.stringify(this.product);
    this.http.post<Product>(this.baseUrl + 'api/Product', body , {'headers':headers}).subscribe(data => {        
    })

    this.table.renderRows();
    
  }
  updateRowData(newRow){
    this.dataSource = this.dataSource.filter((value,key)=>{
      if(value.id == newRow.id){
          value.name = newRow.name;
          value.age=newRow.age;
          value.designation=newRow.designation;
          value.mobileNumber=newRow.mobileNumber;
      }

      this.product = {    
        id: newRow.id,
        name:newRow.name,
        description: newRow.description,
      };
      const headers = { 'content-type': 'application/json'}  
      const body=JSON.stringify(this.product);
      this.http.post<Product[]>(this.baseUrl + 'api/Product/editProduct/' + newRow.id, body , {'headers':headers}).subscribe(data => {         
      })
      return true;
    });
  }
  deleteRowData(newRow){
    this.dataSource = this.dataSource.filter((value,key)=>{
       this.http.delete<any>(this.baseUrl + 'api/Product' + newRow.id).subscribe(data => {         
      })
      return value.id != newRow.id;
    });
  }
}


interface Product {
  id: number;
  name: string;
  description: string;
}