import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { PurchasesComponent } from './purchases/purchases.component';
import { FavoritesComponent } from './favorites/favorites.component';
import { ReviewsComponent } from './reviews/reviews.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { UserComponent } from './user.component';
import { EditUserComponent } from './edit-user/edit-user.component';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    PurchasesComponent,
    FavoritesComponent,
    ReviewsComponent,
    UserDetailsComponent,
    UserComponent,
    EditUserComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    SharedModule
  ]
})
export class UserModule { }