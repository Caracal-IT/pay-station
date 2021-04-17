import { Component, Listen } from '@stencil/core';

@Component({
  tag: 'resize-handle',
  styleUrl: 'resize-handle.scss',
  shadow: true,
})
export class ResizeHandle {
  canResize = false;

  @Listen('mousedown', { capture: true })
  onMouseDown() {
    this.canResize = true;
  }

  @Listen('mouseup', { capture: true })
  onMouseUp() {
    this.canResize = false;
  }

  @Listen('mousemove', { capture: true })
  onMouseMove(e) {
    if(!this.canResize)
      return;

    let headerBeingResized = e.target.parentNode;
    let horizontalScrollOffset = document.documentElement.scrollLeft;
    const width = horizontalScrollOffset + e.clientX - headerBeingResized.offsetLeft;
    e.target.parentNode.width = width - 15 + 'px';
  }

  @Listen('mouseout', { capture: true })
  onMouseOut() {
    this.canResize = false;
  }
}
