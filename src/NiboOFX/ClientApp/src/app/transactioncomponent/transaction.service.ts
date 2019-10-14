import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TransactionT as ResponseTransactions} from './transaction.model';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  private url = "http://localhost:61056/api/SampleData/Transactions";
  constructor(private http: HttpClient) { }

  getTransactions() : Observable<ResponseTransactions>{
    let teste =  this.http.get<ResponseTransactions>(this.url); 
    console.log(teste)
      return teste;
        
  }
}
