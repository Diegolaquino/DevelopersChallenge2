import { Component, OnInit } from '@angular/core';
import { TransactionT as ResponseTransactions} from './transaction.model';
import { TransactionService } from './transaction.service'; 

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent implements OnInit {

  responseTransactions : ResponseTransactions;
  constructor(private transactionService : TransactionService) { }

  ngOnInit() {
    this.transactionService.getTransactions().subscribe(res => this.responseTransactions = res);
  }

}

