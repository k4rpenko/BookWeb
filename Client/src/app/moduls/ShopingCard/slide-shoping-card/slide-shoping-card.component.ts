import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { CartService } from '../../../data/card/cart.service';
import { CommonModule } from '@angular/common';

interface CartItem {
  id: string | undefined;
  title: string | undefined;
  price: number;
  image: string | undefined;
  quantity: number;
}

@Component({
  selector: 'app-slide-shoping-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './slide-shoping-card.component.html',
  styleUrl: './slide-shoping-card.component.scss'
})
export class SlideShopingCardComponent {
  cartItems: CartItem[] = [];
  valid: boolean = true;
  Total: number = 0;
  constructor(public dialogRef: MatDialogRef<SlideShopingCardComponent>, private cartService: CartService) { this.Total = this.cartService.getTotal();  }

  ngOnInit() {
    this.cartItems = this.cartService.getBook();
  }

  loadCartItems(): void {
    this.cartItems = this.cartService.getBook();
  }

  onClose(): void {
    this.dialogRef.close();
  }

  AddQuantity(item: CartItem): void {
    if(this.cartService.getTotal() >= 2000000 || this.cartService.getTotal() + item.price >= 2000000){ this.valid = false; }
    if(this.valid == true){
      this.cartService.AddToCard(item);
      this.cartItems = this.cartService.getBook();
    }
    this.Total = this.cartService.getTotal();
  }
  
  DeleteQuantity(item: CartItem): void {
    this.cartService.DeleteOneQuantity(item);
    this.Total = this.cartService.getTotal();
    this.valid == true
  }

  Removebook(item: CartItem) {
    this.cartService.removeFromCart(item.id === undefined ? "" : item.id)
    this.loadCartItems();  
    this.Total = this.cartService.getTotal();
  }
}
