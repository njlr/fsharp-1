ImplFile
  (ParsedImplFileInput
     ("/root/Expression/If 06.fs", false, QualifiedNameOfFile Module, [], [],
      [SynModuleOrNamespace
         ([Module], false, NamedModule,
          [Expr
             (Do
                (IfThenElse
                   (Const (Bool true, (4,7--4,11)),
                    ArbitraryAfterError ("ifThen3", (4,16--4,16)), None,
                    Yes (4,4--4,16), false, (4,4--4,16),
                    { IfKeyword = (4,4--4,6)
                      IsElif = false
                      ThenKeyword = (4,12--4,16)
                      ElseKeyword = None
                      IfToThenRange = (4,4--4,16) }), (3,0--4,16)), (3,0--4,16))],
          PreXmlDoc ((1,0), FSharp.Compiler.Xml.XmlDocCollector), [], None,
          (1,0--4,16), { LeadingKeyword = Module (1,0--1,6) })], (true, true),
      { ConditionalDirectives = []
        CodeComments = [] }, set []))

(6,0)-(6,4) parse warning Possible incorrect indentation: this token is offside of context started at position (4:5). Try indenting this token further or using standard formatting conventions.
(6,0)-(6,4) parse warning Possible incorrect indentation: this token is offside of context started at position (4:5). Try indenting this token further or using standard formatting conventions.
(6,0)-(6,4) parse error Unexpected keyword 'open' in if/then/else expression
(7,0)-(7,0) parse error Incomplete structured construct at or before this point in definition. Expected incomplete structured construct at or before this point or other token.
(7,0)-(7,0) parse error Incomplete structured construct at or before this point in implementation file
