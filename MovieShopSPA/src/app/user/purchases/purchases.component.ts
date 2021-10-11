import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user.service';
import { PurchaseDetails } from 'src/app/shared/models/PurchaseDetails';
import { Purchases } from 'src/app/shared/models/purchases';

@Component({
  selector: 'app-purchases',
  templateUrl: './purchases.component.html',
  styleUrls: ['./purchases.component.css']
})
export class PurchasesComponent implements OnInit {

  purchases!: Purchases;
  constructor(private userService: UserService) { }
  purchaseDetails!: PurchaseDetails;

  ngOnInit(): void {
    this.userService.getPurchasedMovies().subscribe(resp => {
      this.purchases = resp;
    });

    











  }


}
