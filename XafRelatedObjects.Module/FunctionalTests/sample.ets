;#DropDB XafRelatedObjectsEasyTest

#Application XafRelatedObjectsWin
#Application XafRelatedObjectsBlazor

*FillForm
 User Name = Admin
*Action Log In

#IfDef XafRelatedObjectsWin

*Action About...
 
*Action OK

#EndIf

#IfDef XafRelatedObjectsBlazor
*Action Navigation(My Details)
#EndIf
