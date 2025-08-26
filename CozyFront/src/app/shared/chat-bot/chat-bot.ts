import { Component } from '@angular/core';

@Component({
  selector: 'app-chat-bot',
  standalone: false,
  templateUrl: './chat-bot.html',
  styleUrl: './chat-bot.css'
})
export class ChatBot {
  isOpen = false;

  openChat() {
    this.isOpen = true;
  }

  closeChat() {
    this.isOpen = false;
  }
}
