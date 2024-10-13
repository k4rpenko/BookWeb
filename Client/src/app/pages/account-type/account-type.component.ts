import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from "@angular/common";
import { FormsModule } from '@angular/forms';
import { EmailValid } from "../../data/POST/AccountCheckValid/EmailValid";
import { Router } from '@angular/router';

@Component({
  selector: 'app-account-type',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './account-type.component.html',
  styleUrls: ['./account-type.component.scss']
})
export class AccountTypeComponent implements OnInit {
  token: string | null = null;
  actionType: string | null = null;
  
  oldPassword: string = '';    
  newPassword: string = '';    

  constructor(private route: ActivatedRoute, private router: Router) {}

  profileService = inject(EmailValid);
  ValidEmail(){}
  
  ngOnInit(): void {
    this.token = this.route.snapshot.paramMap.get('token');
    this.actionType = this.route.snapshot.paramMap.get('type'); 

    if (this.actionType === 'action' && this.token) {
      this.profileService.PostValidToken(this.token).subscribe({
        next: (response) => {
          console.log('Запит успішний:', response);
          this.router.navigate(['/']);
        },
        error: (error) => {
          console.error('Помилка при виконанні POST запиту:', error);
        }
      });
    } else if (this.actionType === 'change') {
      console.log('Зміна пароля активована');
    }
  }
}
