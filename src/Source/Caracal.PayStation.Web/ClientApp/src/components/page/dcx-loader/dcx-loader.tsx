import {Component, Prop, Listen, h, State} from '@stencil/core';
import {Context} from 'caracal_polaris/dist/types/model/context.model';

@Component({
  tag: 'dcx-loader',
  styleUrl: 'dcx-loader.scss',
  shadow: true,
})
export class DcxLoader {
  showLoader = false;

  @Prop() ctx: Context;
  @State() isVisible = false;

  @Listen('wfMessage', { capture: true, target: 'document' })
  wfMessage(event: CustomEvent){
    const msg = event.detail;

    switch (msg.type) {
      case "ERROR": return alert(msg.description||msg.metadata.error.error.message);
      case "START_LOADING": return this.show(true);
      case "END_LOADING": return this.show(false);
    }
  }

  show(isVisible) {
    this.showLoader = isVisible;

    setTimeout(() => this.isVisible = this.showLoader, 300);
  }

  render(){
    return this.isVisible ? <div/> : null;
  }
}
