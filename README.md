# draig-dwyren

Experiment combining concepts from DRAKON and Erlang/OTP

## Purpose

- A system to combine planning and implementation of software ideas
- A graphic design language that covers processes, state machines, distributed systems
- Not targeted toward human or general processes (unlike DRAKON)
- Minimise number of modes

### References

- https://drakonhub.com/en/drakon-reference
- https://drakon.tech/read/programming_in_drakon


## Notes

- We should have our own set of "icons", don't try to be quite as abstract and brutalist as drakon.
- Loops can be either explicit with `goto`s, or done as batches of work.
- Interprocess messaging should be core to IO
- 'Silhouettes' and 'async' can be replaces with state machines. Use better graphic representation that traditional ball-and-line.

## Ideas

Split the layout of processes from the SVG rendering, using https://jsfiddle.net/i_e_b/L9v16acs/
- Implement *Quick-n-Dirty* Diagrams in C#
- The `DraigCore` outputs a process as a QnD program