import { Component, Listen, Prop, h } from '@stencil/core';
import {Context} from 'caracal_polaris/dist/types/model/context.model';

@Component({
  tag: 'apx-button',
  styleUrl: 'apx-button.scss',
  shadow: true,
})
export class ApexButton {
  @Prop() caption: string;
  @Prop() ctx: Context;
  @Prop() next: string;
  @Prop() isDefault: boolean = false;

  private buttonHandler() {
    this.ctx.wf.goto(this.next);
  }

  @Listen('keydown', { capture: true, target: 'window' })
  keydownHandler(event: KeyboardEvent) {
    if (this.isDefault && event.key === 'Enter')
      this.buttonHandler();
  }

  render() {
    return (
      <button onClick={this.buttonHandler.bind(this)}>{this.caption}</button>
    );
  }
}
